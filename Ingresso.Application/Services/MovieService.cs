using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.CloudinaryConfigClass;
using Ingresso.Infra.Data.UtilityExternal.Interface;

namespace Ingresso.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieDTOValidator _validator;
        private readonly ICloudinaryUti _cloudinaryUti;
        private readonly IMovieTheaterService _movieTheaterService;
        private readonly IRegionService _regionService;

        private readonly Account _account = new Account(
            CloudinaryConfig.AccountName,
            CloudinaryConfig.ApiKey,
            CloudinaryConfig.ApiSecret);

        public MovieService(
            IMovieRepository movieRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IMovieDTOValidator movieDTOValidator, ICloudinaryUti cloudinaryUti, IMovieTheaterService movieTheaterService, IRegionService regionService)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = movieDTOValidator;
            _cloudinaryUti = cloudinaryUti;
            _movieTheaterService = movieTheaterService;
            _regionService = regionService;
        }

        public async Task<ResultService<List<MovieDTO>>> GetAllMovieByRegionId(string region)
        {
            var idRegion = await _regionService.GetIdByNameCity(region);

            if(!idRegion.IsSucess || idRegion.Data == null)
                return ResultService.Fail(_mapper.Map<List<MovieDTO>>($"{idRegion.Message}"));

            // a ideia aqui é pegar o nome e pegar da tabela region o id
            var theatreAll = await _movieRepository.GetAllMovieByRegionId(idRegion.Data.Id);

            return ResultService.Ok(_mapper.Map<List<MovieDTO>>(theatreAll));
        }

        public async Task<ResultService<MovieDTO>> GetInfoMoviesById(Guid id)
        {
            var movie = await _movieRepository.GetInfoMoviesById(id);

            return ResultService.Ok(_mapper.Map<MovieDTO>(movie));
        }

        public async Task<ResultService<MovieDTO>> GetStatusMovie(string statusMovie)
        {
            var theatreAll = await _movieRepository.GetStatusMovie(statusMovie);

            if (theatreAll == null)
                return ResultService.Fail<MovieDTO>("statusMovie not fould");

            return ResultService.Ok(_mapper.Map<MovieDTO>(theatreAll));
        }

        public async Task<ResultService<MovieDTO>> Create(MovieDTO? movieDTO)
        {
            if (movieDTO == null)
                return ResultService.Fail<MovieDTO>("error DTO NULL");

            var validator = _validator.ValidateDTO(movieDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<MovieDTO>("error validate DTO verific prop", validator);

            Guid idUser = Guid.NewGuid();

            movieDTO.Id = idUser;

            var result = await _cloudinaryUti.CreateImg(movieDTO.Base64Img ?? "", 625, 919);
            var resultImgBackground = await _cloudinaryUti.CreateImg(movieDTO.ImgUrlBackground ?? "", 1440, 500);

            if (resultImgBackground.ImgUrl == null || resultImgBackground.PublicId == null)
                return ResultService.Fail<MovieDTO>("erro ao criar imgBackground");

            if (result.ImgUrl == null || result.PublicId == null)
                return ResultService.Fail<MovieDTO>("erro ao criar imgMain");

            movieDTO.ImgUrl = result.ImgUrl;
            movieDTO.PublicId = result.PublicId;

            movieDTO.ImgUrlBackground = resultImgBackground.ImgUrl;
            movieDTO.PublicIdImgBackgound = resultImgBackground.PublicId;

            try
            {
                await _unitOfWork.BeginTransaction();
                var movieCreate = await _movieRepository.Create(_mapper.Map<Movie>(movieDTO));

                if (movieCreate == null)
                {
                    await _unitOfWork.Rollback();
                    return ResultService.Fail<MovieDTO>("error when creating Movie null database");
                }

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<MovieDTO>(movieCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<MovieDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<MovieDTO>> DeleteMovie(Guid guidId)
        {
            var movieDelete = await _movieRepository.GetByIdAllProps(guidId);
            if (movieDelete == null)
                return ResultService.Fail<MovieDTO>("error encontrar Movie");

            var deleteMovieThe = await _movieTheaterService.Delete(guidId);
            if (deleteMovieThe.Message == "Algum erro na hora de deletar verifique")
                return ResultService.Fail<MovieDTO>("não foi possivel deletar 'theaterService' verifique o porque");

            var cloudinary = new Cloudinary(_account);

            try
            {
                await _unitOfWork.BeginTransaction();
                var deleteMovie = await _movieRepository.Delete(movieDelete);

                if (deleteMovie.PublicId != null)
                {
                    var destroyParams = new DeletionParams(deleteMovie.PublicId) { ResourceType = ResourceType.Image };
                    await cloudinary.DestroyAsync(destroyParams);
                }

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<MovieDTO>(deleteMovie));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<MovieDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<MovieDTO>> UpdateMovie(MovieDTO movieDTO)
        {
            if (movieDTO.Id == null || movieDTO.Base64Img == null)
                return ResultService.Fail<MovieDTO>("Guid null ou base64Img não informado");

            var movieUpdate = await _movieRepository.GetByIdAllProps(movieDTO.Id.Value);
            if (movieUpdate == null)
                return ResultService.Fail<MovieDTO>("error encontrar Movie");

            var cloudinary = new Cloudinary(_account);

            var result = await _cloudinaryUti.CreateImg(movieDTO.Base64Img ?? "", 625, 919);

            if (result.ImgUrl == null || result.PublicId == null)
                return ResultService.Fail<MovieDTO>("erro ao criar img");
            
            try
            {
                await _unitOfWork.BeginTransaction();

                if (movieUpdate.PublicId != null)
                {
                    var destroyParams = new DeletionParams(movieUpdate.PublicId) { ResourceType = ResourceType.Image };
                    cloudinary.Destroy(destroyParams);
                }

                movieUpdate.SetImgUrlPublicId(result.ImgUrl, result.PublicId);

                var movieBdUpdate = await _movieRepository.Update(movieUpdate);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<MovieDTO>(movieBdUpdate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<MovieDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<MovieDTO>> UpdateMovieImgBackground(MovieDTO movieDTO)
        {
            if (movieDTO.Id == null || movieDTO.ImgUrlBackground == null)
                return ResultService.Fail<MovieDTO>("Guid null ou base64Img não informado");

            var movieUpdate = await _movieRepository.GetByIdAllProps(movieDTO.Id.Value);
            if (movieUpdate == null)
                return ResultService.Fail<MovieDTO>("error encontrar Movie");

            var result = await _cloudinaryUti.CreateImg(movieDTO.ImgUrlBackground ?? "", 1440, 500);

            if (result.ImgUrl == null || result.PublicId == null)
                return ResultService.Fail<MovieDTO>("erro ao criar img");

            try
            {
                await _unitOfWork.BeginTransaction();

                movieUpdate.SetImgBackgroundPublicId(result.ImgUrl, result.PublicId);

                var movieBdUpdate = await _movieRepository.Update(movieUpdate);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<MovieDTO>(movieBdUpdate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<MovieDTO>($"{ex.Message}");
            }
        }
    }
}

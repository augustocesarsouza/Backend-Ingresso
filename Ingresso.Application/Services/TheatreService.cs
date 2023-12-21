using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using System.Globalization;
using Ingresso.Infra.Data.CloudinaryConfigClass;
using Ingresso.Infra.Data.UtilityExternal.Interface;
using Ingresso.Infra.Data.Repositories;

namespace Ingresso.Application.Services
{
    public class TheatreService : ITheatreService
    {
        private readonly ITheatreRepository _theatreRepository;
        private readonly ITheatreDTOValidator _theatreDTOValidator;
        private readonly IRegionTheatreService _theatreService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryUti _cloudinaryUti;
        private readonly IRegionService _regionService;

        private readonly Account _account = new Account(
           CloudinaryConfig.AccountName,
           CloudinaryConfig.ApiKey,
           CloudinaryConfig.ApiSecret);

        public TheatreService(
            ITheatreRepository theatreRepository, IUnitOfWork unitOfWork, IMapper mapper,
            ITheatreDTOValidator theatreDTOValidator, IRegionTheatreService theatreService, ICloudinaryUti cloudinaryUti, IRegionService regionService)
        {
            _theatreRepository = theatreRepository;
            _theatreDTOValidator = theatreDTOValidator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _theatreService = theatreService;
            _cloudinaryUti = cloudinaryUti;
            _regionService = regionService;
        }

        public async Task<ResultService<List<TheatreDTO>>> GetAllTheatreByRegionId(string region)
        {
            var idRegion = await _regionService.GetIdByNameCity(region);
            if (!idRegion.IsSucess || idRegion.Data == null)
                return ResultService.Fail(_mapper.Map<List<TheatreDTO>>($"{idRegion.Message}"));

            var theatreAll = await _theatreRepository.GetAllTheatreByRegionId(idRegion.Data.Id);

            return ResultService.Ok(_mapper.Map<List<TheatreDTO>>(theatreAll));
        }

        public async Task<ResultService<TheatreDTO>> Create(TheatreDTO theatreDTO)
        {
            if (theatreDTO == null)
                return ResultService.Fail<TheatreDTO>("objeto para criação do theatre null");

            var result = _theatreDTOValidator.ValidateDTO(theatreDTO);
            if (!result.IsValid)
                return ResultService.RequestError<TheatreDTO>("falha validar objeto", result);

            var guidId = Guid.NewGuid();

            var theatheGet = await _theatreRepository.GetById(guidId);
            if (theatheGet != null)
                return ResultService.Fail<TheatreDTO>("já existe um Id parecido no database");

            theatreDTO.Id = guidId;

            var resultCreateImg = await _cloudinaryUti.CreateImg(theatreDTO.Base64Img ?? "", 590, 320);

            theatreDTO.ImgUrl = resultCreateImg.ImgUrl;
            theatreDTO.PublicId = resultCreateImg.PublicId;

            if (theatreDTO.DataString != null)
            {
                var formatoDataHora = "dd/MM/yyyy HH:mm";

                var birthDate = DateTime.ParseExact(theatreDTO.DataString, formatoDataHora, CultureInfo.InvariantCulture);

                theatreDTO.Data = birthDate;
            }

            try
            {
                await _unitOfWork.BeginTransaction();

                var theatreCreate = await _theatreRepository.Create(_mapper.Map<Theatre>(theatreDTO));

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<TheatreDTO>(theatreCreate));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<TheatreDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<TheatreDTO>> UpdateMovie(TheatreDTO theatreDTO)
        {
            if (theatreDTO.Id == null || theatreDTO.Base64Img == null)
                return ResultService.Fail<TheatreDTO>("Guid null ou base64Img não informado");

            var theatreUpdate = await _theatreRepository.GetByIdAllProps(theatreDTO.Id.Value);
            if (theatreUpdate == null)
                return ResultService.Fail<TheatreDTO>("error encontrar Movie");

            var cloudinary = new Cloudinary(_account);

            var result = await _cloudinaryUti.CreateImg(theatreDTO.Base64Img ?? "", 590, 320);

            if (result.ImgUrl == null || result.PublicId == null)
                return ResultService.Fail<TheatreDTO>("erro ao criar img");

            try
            {
                await _unitOfWork.BeginTransaction();

                if (theatreUpdate.PublicId != null)
                {
                    var destroyParams = new DeletionParams(theatreUpdate.PublicId) { ResourceType = ResourceType.Image };
                    cloudinary.Destroy(destroyParams);
                }

                theatreUpdate.SetImgUrlPublicId(result.ImgUrl, result.PublicId);

                var movieBdUpdate = await _theatreRepository.Update(theatreUpdate);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<TheatreDTO>(movieBdUpdate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<TheatreDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<TheatreDTO>> DeleteTheatre(Guid guidId)
        {
            var theatreDelete = await _theatreRepository.GetByIdAllProps(guidId);
            if (theatreDelete == null)
                return ResultService.Fail<TheatreDTO>("error encontrar Movie");

            var deleteMovieThe = await _theatreService.Delete(guidId);
            if (deleteMovieThe.Message == "Algum erro na hora de deletar verifique")
                return ResultService.Fail<TheatreDTO>("não foi possivel deletar 'theaterService' verifique o porque");

            var cloudinary = new Cloudinary(_account);

            try
            {
                await _unitOfWork.BeginTransaction();

                var deleteMovie = await _theatreRepository.Delete(theatreDelete);

                if (deleteMovie.PublicId != null)
                {
                    var destroyParams = new DeletionParams(deleteMovie.PublicId) { ResourceType = ResourceType.Image };
                    await cloudinary.DestroyAsync(destroyParams);
                }

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<TheatreDTO>(deleteMovie));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<TheatreDTO>($"{ex.Message}");
            }
        }


    }
}

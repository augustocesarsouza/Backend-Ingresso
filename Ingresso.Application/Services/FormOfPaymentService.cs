using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;

namespace Ingresso.Application.Services
{
    public class FormOfPaymentService : IFormOfPaymentService
    {
        private readonly IFormOfPaymentRepository _formOfPaymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFormOfPaymentDTOValidator _formOfPaymentDTOValidator;

        public FormOfPaymentService(IFormOfPaymentRepository formOfPaymentRepository, IUnitOfWork unitOfWork, IMapper mapper, IFormOfPaymentDTOValidator formOfPaymentDTOValidator)
        {
            _formOfPaymentRepository = formOfPaymentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _formOfPaymentDTOValidator = formOfPaymentDTOValidator;
        }

        public async Task<ResultService<List<FormOfPaymentDTO>>> GetMovieIDInfo(Guid movieId)
        {
            var formList = await _formOfPaymentRepository.GetMovieIDInfo(movieId);
            return ResultService.Ok(_mapper.Map<List<FormOfPaymentDTO>>(formList));
        }

        public async Task<ResultService<FormOfPaymentDTO>> Create(FormOfPaymentDTO? formOfPaymentDTO)
        {
            if (formOfPaymentDTO == null)
                return ResultService.Fail<FormOfPaymentDTO>("obj informed is null a valid obj must be informed");

            var valid = _formOfPaymentDTOValidator.ValidateDTO(formOfPaymentDTO);
            if (!valid.IsValid)
                return ResultService.RequestError<FormOfPaymentDTO>("verify obj informed some invalid information", valid);

            Guid idUser = Guid.NewGuid();
            formOfPaymentDTO.Id = idUser;

            try
            {
                await _unitOfWork.BeginTransaction();
                var formCreate = await _formOfPaymentRepository.Create(_mapper.Map<FormOfPayment>(formOfPaymentDTO));
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<FormOfPaymentDTO>(formCreate));

            }
            catch(Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<FormOfPaymentDTO>($"{ex.Message}");
            }
        }
    }
}

namespace Ingresso.Application.DTOs.Validations.Interfaces
{
    public interface IFormOfPaymentDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(FormOfPaymentDTO formOfPaymentDTO);
    }
}

namespace Ingresso.Application.DTOs.Validations.Interfaces
{
    public interface IAdditionalInfoUserDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(AdditionalInfoUserDTO infoUserDTO);
    }
}

namespace Ingresso.Application.DTOs.Validations.Interfaces
{
    public interface IUserCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(UserDto userDto);
    }
}

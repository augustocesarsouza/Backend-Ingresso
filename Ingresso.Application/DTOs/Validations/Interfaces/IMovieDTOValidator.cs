namespace Ingresso.Application.DTOs.Validations.Interfaces
{
    public interface IMovieDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(MovieDTO movieDTO);
    }
}

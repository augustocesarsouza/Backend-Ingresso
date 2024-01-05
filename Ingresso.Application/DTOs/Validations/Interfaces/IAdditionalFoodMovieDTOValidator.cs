namespace Ingresso.Application.DTOs.Validations.Interfaces
{
    public interface IAdditionalFoodMovieDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(AdditionalFoodMovieDTO additionalFoodMovieDTO);
    }
}

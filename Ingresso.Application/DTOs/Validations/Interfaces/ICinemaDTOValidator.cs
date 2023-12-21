namespace Ingresso.Application.DTOs.Validations.Interfaces
{
    public interface ICinemaDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(CinemaDTO cinemaDTO);
    }
}

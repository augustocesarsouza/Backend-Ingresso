namespace Ingresso.Application.DTOs.Validations.Interfaces
{
    public interface ICinemaMovieDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(CinemaMovieDTO cinemaMovieDTO);
    }
}

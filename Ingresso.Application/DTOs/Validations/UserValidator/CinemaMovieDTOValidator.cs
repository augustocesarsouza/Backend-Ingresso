using FluentValidation;
using FluentValidation.Results;
using Ingresso.Application.DTOs.Validations.Interfaces;

namespace Ingresso.Application.DTOs.Validations.UserValidator
{
    public class CinemaMovieDTOValidator : AbstractValidator<CinemaMovieDTO>, ICinemaMovieDTOValidator
    {
        public CinemaMovieDTOValidator()
        {
            RuleFor(x => x.CinemaId)
                .NotEmpty()
                .NotNull()
                .WithMessage("CinemaId movie should be informed");

            RuleFor(x => x.MovieId)
                .NotEmpty()
                .NotNull()
                .WithMessage("MovieId movie should be informed");

            RuleFor(x => x.ScreeningSchedule)
                .NotEmpty()
                .NotNull()
                .WithMessage("ScreeningSchedule movie should be informed");
        }

        public ValidationResult ValidateDTO(CinemaMovieDTO cinemaMovieDTO)
        {
            return Validate(cinemaMovieDTO);
        }
    }
}

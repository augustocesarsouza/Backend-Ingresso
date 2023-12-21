using FluentValidation;
using FluentValidation.Results;
using Ingresso.Application.DTOs.Validations.Interfaces;

namespace Ingresso.Application.DTOs.Validations.UserValidator
{
    public class CinemaDTOValidator : AbstractValidator<CinemaDTO>, ICinemaDTOValidator
    {
        public CinemaDTOValidator()
        {
            RuleFor(x => x.NameCinema)
                .NotEmpty()
                .NotNull()
                .WithMessage("NameCinema movie should be informed");

            RuleFor(x => x.District)
                .NotEmpty()
                .NotNull()
                .WithMessage("District movie should be informed");

            RuleFor(x => x.Ranking)
                .NotEmpty()
                .NotNull()
                .WithMessage("Ranking movie should be informed");
        }

        public ValidationResult ValidateDTO(CinemaDTO cinemaDTO)
        {
            return Validate(cinemaDTO);
        }
    }
}

using FluentValidation;
using FluentValidation.Results;
using Ingresso.Application.DTOs.Validations.Interfaces;

namespace Ingresso.Application.DTOs.Validations.UserValidator
{
    public class AdditionalFoodMovieDTOValidator : AbstractValidator<AdditionalFoodMovieDTO>, IAdditionalFoodMovieDTOValidator
    {
        public AdditionalFoodMovieDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("a title shall be provided");

            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("a Price shall be provided");

            RuleFor(x => x.Fee)
                .NotEmpty()
                .NotNull()
                .WithMessage("a Fee shall be provided");

            RuleFor(x => x.MovieId)
                .NotEmpty()
                .NotNull()
                .WithMessage("a MovieId shall be provided");

            RuleFor(x => x.Base64Img)
                .NotEmpty()
                .NotNull()
                .WithMessage("a img shall be provided");
        }

        public ValidationResult ValidateDTO(AdditionalFoodMovieDTO additionalFoodMovieDTO)
        {
            return Validate(additionalFoodMovieDTO);
        }
    }
}

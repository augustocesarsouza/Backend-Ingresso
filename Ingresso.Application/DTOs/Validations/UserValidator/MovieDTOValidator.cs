using FluentValidation;
using FluentValidation.Results;
using Ingresso.Application.DTOs.Validations.Interfaces;

namespace Ingresso.Application.DTOs.Validations.UserValidator
{
    public class MovieDTOValidator : AbstractValidator<MovieDTO>, IMovieDTOValidator
    {
        public MovieDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Title movie should be informed");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Description movie should be informed");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .NotNull()
                .WithMessage("Gender movie should be informed");

            RuleFor(x => x.Duration)
                .NotEmpty()
                .NotNull()
                .WithMessage("Duration movie should be informed");

            RuleFor(x => x.MovieRating)
                .GreaterThan(0)
                .WithMessage("MovieRating movie should be GreaterThan 0");

            RuleFor(x => x.Base64Img)
                .NotEmpty()
                .NotNull()
                .WithMessage("Base64Img should be informed for create Img Cloudinary");
        }

        public ValidationResult ValidateDTO(MovieDTO movieDTO)
        {
            return Validate(movieDTO);
        }
    }
}

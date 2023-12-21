using FluentValidation;
using FluentValidation.Results;
using Ingresso.Application.DTOs.Validations.Interfaces;

namespace Ingresso.Application.DTOs.Validations.UserValidator
{
    public class TheatreDTOValidator : AbstractValidator<TheatreDTO>, ITheatreDTOValidator
    {
        public TheatreDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Title should be informed");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Description should be informed");

            RuleFor(x => x.DataString)
                .NotEmpty()
                .NotNull()
                .WithMessage("DataString should be informed");

            RuleFor(x => x.Location)
                .NotEmpty()
                .NotNull()
                .WithMessage("Location should be informed");

            RuleFor(x => x.TypeOfAttraction)
                .NotEmpty()
                .NotNull()
                .WithMessage("TypeOfAttraction should be informed");

            RuleFor(x => x.Category)
                .NotEmpty()
                .NotNull()
                .WithMessage("Category should be informed");

            RuleFor(x => x.Base64Img)
                .NotEmpty()
                .NotNull()
                .WithMessage("Base64Img should be informed for create Img Cloudinary");
        }

        public ValidationResult ValidateDTO(TheatreDTO theatreDTO)
        {
            return Validate(theatreDTO);
        }
    }
}

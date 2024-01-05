using FluentValidation;
using FluentValidation.Results;
using Ingresso.Application.DTOs.Validations.Interfaces;

namespace Ingresso.Application.DTOs.Validations.UserValidator
{
    public class FormOfPaymentDTOValidator : AbstractValidator<FormOfPaymentDTO>, IFormOfPaymentDTOValidator
    {
        public FormOfPaymentDTOValidator()
        {
            RuleFor(x => x.FormName)
                .NotEmpty()
                .NotNull()
                .WithMessage("should be informed one name for formName");

            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("should be informed one price for 'Price'");

            RuleFor(x => x.MovieId)
               .NotEmpty()
               .NotNull()
               .WithMessage("should be informed one ID of a movie for MovieId");
        }

        public ValidationResult ValidateDTO(FormOfPaymentDTO formOfPaymentDTO)
        {
            return Validate(formOfPaymentDTO);
        }
    }
}

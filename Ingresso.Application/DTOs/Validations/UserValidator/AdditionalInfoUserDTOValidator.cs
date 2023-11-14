using FluentValidation;
using FluentValidation.Results;
using Ingresso.Application.DTOs.Validations.Interfaces;

namespace Ingresso.Application.DTOs.Validations.UserValidator
{
    public class AdditionalInfoUserDTOValidator : AbstractValidator<AdditionalInfoUserDTO>, IAdditionalInfoUserDTOValidator
    {
        public AdditionalInfoUserDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Valor do id do usuario deve ser maior que 0");
        }

        public ValidationResult ValidateDTO(AdditionalInfoUserDTO infoUserDTO)
        {
            return Validate(infoUserDTO);
        }
    }
}

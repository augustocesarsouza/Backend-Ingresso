using FluentValidation;
using FluentValidation.Results;
using Ingresso.Application.DTOs.Validations.Interfaces;

namespace Ingresso.Application.DTOs.Validations.UserValidator
{
    public class UserCreateDTOValidator : AbstractValidator<UserDto>, IUserCreateDTOValidator
    {
        public UserCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Should be Informed Name");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Should be Informed Email");

            RuleFor(x => x.Cpf)
                .NotEmpty()
                .NotNull()
                .WithMessage("Should be Informed Cpf");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(7, 20)
                .WithMessage("The password must contain a minimum of 7 characters and a maximum of 20");
        }

        public ValidationResult ValidateDTO(UserDto userDto)
        {
            return Validate(userDto);
        }
    }
}

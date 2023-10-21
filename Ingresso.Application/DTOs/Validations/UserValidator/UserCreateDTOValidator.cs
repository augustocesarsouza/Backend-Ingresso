using FluentValidation;

namespace Ingresso.Application.DTOs.Validations.UserValidator
{
    public class UserCreateDTOValidator : AbstractValidator<UserDto>
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

            RuleFor(x => x.EmailRecovery)
                .NotEmpty()
                .NotNull()
                .WithMessage("Should be Informed EmailRecovery");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull()
                .WithMessage("Should be Informed Phone");

            RuleFor(x => x.Cpf)
                .NotEmpty()
                .NotNull()
                .WithMessage("Should be Informed Cpf");

            RuleFor(x => x.BirthDateString)
                .NotEmpty()
                .NotNull()
                .WithMessage("Should be Informed BirthDate");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(7, 20)
                .WithMessage("The password must contain a minimum of 7 characters and a maximum of 20");
        }
    }
}

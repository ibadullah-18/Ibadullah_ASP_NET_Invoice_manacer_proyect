using FluentValidation;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.User;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name boş ola bilməz")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz")
            .EmailAddress().WithMessage("Email formatı düzgün deyil");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password boş ola bilməz")
            .MinimumLength(6).WithMessage("Password ən az 6 simvol olmalıdır");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20);

        RuleFor(x => x.Address)
            .MaximumLength(250);
    }
}
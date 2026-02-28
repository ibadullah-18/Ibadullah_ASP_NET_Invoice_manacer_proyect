using FluentValidation;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.User;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz")
            .EmailAddress().WithMessage("Email formatı düzgün deyil");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password boş ola bilməz");
    }
}   
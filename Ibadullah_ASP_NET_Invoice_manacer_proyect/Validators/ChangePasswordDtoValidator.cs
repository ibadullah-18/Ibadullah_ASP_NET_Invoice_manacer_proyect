using FluentValidation;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.User;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Validators;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Əvvəlki parol boş ola bilməz");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Yeni parol boş ola bilməz")
            .MinimumLength(6).WithMessage("Yeni parol ən az 6 simvol olmalıdır");
    }
}
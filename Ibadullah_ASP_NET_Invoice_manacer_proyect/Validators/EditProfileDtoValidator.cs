using FluentValidation;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.User;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Validators;

public class EditProfileDtoValidator : AbstractValidator<EditProfileDto>
{
    public EditProfileDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name boş ola bilməz")
            .MaximumLength(100);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20);

        RuleFor(x => x.Address)
            .MaximumLength(250);
    }
}
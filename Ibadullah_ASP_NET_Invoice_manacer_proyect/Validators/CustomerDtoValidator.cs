using FluentValidation;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Customer;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Validators;

public class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz")
            .MaximumLength(100)
            .MinimumLength(3).WithMessage("Ad 3-100 simvol aralığında olmalıdır");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz")
            .EmailAddress().WithMessage("Geçərli bir email adresi daxil edin!");
        RuleFor(x => x.Address)
            .MaximumLength(200);
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Geçərli nömrə daxil edin! (Mes: +994556151345)");
    }
}

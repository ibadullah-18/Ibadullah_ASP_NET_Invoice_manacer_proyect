using FluentValidation;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Validators;

public class CreateInvoiceDtoValidator : AbstractValidator<CreateInvoiceDto>
{
    public CreateInvoiceDtoValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId boş ola bilməz");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("StartDate EndDate-dan böyük ola bilməz");

        RuleFor(x => x.Rows)
            .NotEmpty().WithMessage("Invoice ən az 1 row içerməlidir");

        RuleForEach(x => x.Rows).ChildRules(row =>
        {
            row.RuleFor(r => r.Service)
                .NotEmpty().WithMessage("Service boş ola bilməz");

            row.RuleFor(r => r.Quantity)
                .GreaterThan(0).WithMessage("Quantity 0-dan böyük olmalıdır");

            row.RuleFor(r => r.Amount)
                .GreaterThan(0).WithMessage("Amount 0-dan böyük olmalıdır");
        });
    }
}

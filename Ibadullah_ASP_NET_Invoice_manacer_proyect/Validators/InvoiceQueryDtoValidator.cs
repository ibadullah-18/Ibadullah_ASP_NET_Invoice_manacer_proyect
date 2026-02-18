using FluentValidation;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Validators;

public class InvoiceQueryDtoValidator : AbstractValidator<InvoiceQueryDto>
{
    public InvoiceQueryDtoValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber 1-dən böyük olmalıdır");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize 1-dən böyük olmalıdır");

        RuleFor(x => x.OrderBy)
            .Must(x => string.IsNullOrEmpty(x) || new[] { "CreatedAt", "StartDate", "EndDate" }.Contains(x))
            .WithMessage("OrderBy yalnız CreatedAt, StartDate və EndDate ola bilər");
    }
}

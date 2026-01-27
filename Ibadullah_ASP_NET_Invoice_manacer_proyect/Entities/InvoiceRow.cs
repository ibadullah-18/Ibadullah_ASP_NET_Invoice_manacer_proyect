namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;

public class InvoiceRow : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public string Service { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
    public decimal Sum { get; set; }
}


namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;

public class Invoice : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public decimal TotalSum { get; set; }
    public string? Comment { get; set; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Created;

    public ICollection<InvoiceRow> Rows { get; set; } = new List<InvoiceRow>();
}

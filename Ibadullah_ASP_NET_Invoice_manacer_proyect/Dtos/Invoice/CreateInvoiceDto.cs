namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;

public class CreateInvoiceDto
{
    public Guid CustomerId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public List<CreateInvoiceRowDto> Rows { get; set; }
    public string? Comment { get; set; }
}

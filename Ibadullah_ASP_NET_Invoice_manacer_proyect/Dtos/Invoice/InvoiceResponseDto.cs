namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;

public class InvoiceResponseDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public List<InvoiceRowResponseDto> Rows { get; set; }
    public decimal TotalSum { get; set; }
    public string? Comment { get; set; }
    public string Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

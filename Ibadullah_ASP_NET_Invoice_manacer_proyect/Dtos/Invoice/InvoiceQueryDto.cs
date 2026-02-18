namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;

public class InvoiceQueryDto
{
    public Guid? CustomerId { get; set; }
    public DateTimeOffset? StartDateFrom { get; set; }
    public DateTimeOffset? StartDateTo { get; set; }
    public string Status { get; set; } = string.Empty; 

    public string? OrderBy { get; set; } = "CreatedAt";
    public bool IsDescending { get; set; } = false;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

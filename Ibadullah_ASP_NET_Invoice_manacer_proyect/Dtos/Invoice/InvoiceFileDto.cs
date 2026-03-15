namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;

public class InvoiceFileDto
{
    public byte[] FileBytes { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}
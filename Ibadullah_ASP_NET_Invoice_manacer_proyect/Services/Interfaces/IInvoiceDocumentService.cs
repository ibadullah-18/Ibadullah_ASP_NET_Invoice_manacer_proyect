using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;

public interface IInvoiceDocumentService
{
    Task<InvoiceFileDto?> DownloadInvoiceAsync(Guid invoiceId, string format);
}
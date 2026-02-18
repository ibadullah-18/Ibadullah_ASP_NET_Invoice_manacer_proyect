using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceDto dto);
    Task<InvoiceResponseDto> EditInvoiceAsync(Guid id, CreateInvoiceDto dto);
    Task<bool> ChangeStatusAsync(Guid id, string newStatus);
    Task<bool> DeleteInvoiceAsync(Guid id);
    Task<bool> ArchiveInvoiceAsync(Guid id);
    Task<InvoiceResponseDto?> GetInvoiceByIdAsync(Guid id);
    Task<PagedResponse<InvoiceResponseDto>> GetInvoicesListAsync(InvoiceQueryDto query);

}

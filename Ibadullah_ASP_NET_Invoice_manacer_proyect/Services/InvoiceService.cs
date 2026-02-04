using Ibadullah_ASP_NET_Invoice_manacer_proyect.Data;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Services;

public class InvoiceService : IInvoiceService
{
    private readonly AppDbContext _context;

    public InvoiceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceDto dto)
    {
        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            CustomerId = dto.CustomerId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Comment = dto.Comment,
            Status = InvoiceStatus.Created,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            Rows = dto.Rows.Select(r => new InvoiceRow
            {
                Id = Guid.NewGuid(),
                Service = r.Service,
                Quantity = r.Quantity,
                Amount = r.Amount,
                Sum = r.Quantity * r.Amount
            }).ToList()
        };

        invoice.TotalSum = invoice.Rows.Sum(r => r.Sum);

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return MapToResponseDto(invoice);
    }

    public async Task<InvoiceResponseDto> EditInvoiceAsync(Guid id, CreateInvoiceDto dto)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == id && i.Status == InvoiceStatus.Created);

        if (invoice == null)
            throw new Exception("Invoice tapılmadı və ya göndərilmişdir.");

        invoice.StartDate = dto.StartDate;
        invoice.EndDate = dto.EndDate;
        invoice.Comment = dto.Comment;
        invoice.UpdatedAt = DateTimeOffset.UtcNow;

        _context.InvoiceRows.RemoveRange(invoice.Rows);
        invoice.Rows = dto.Rows.Select(r => new InvoiceRow
        {
            Id = Guid.NewGuid(),
            Service = r.Service,
            Quantity = r.Quantity,
            Amount = r.Amount,
            Sum = r.Quantity * r.Amount
        }).ToList();

        invoice.TotalSum = invoice.Rows.Sum(r => r.Sum);

        await _context.SaveChangesAsync();

        return MapToResponseDto(invoice);
    }

    public async Task<bool> ChangeStatusAsync(Guid id, string newStatus)
    {
        var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
        if (invoice == null)
            return false;

        if (!Enum.TryParse<InvoiceStatus>(newStatus, true, out var status))
            throw new Exception("Status yanlışdır");

        invoice.Status = status;
        invoice.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteInvoiceAsync(Guid id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == id && i.Status == InvoiceStatus.Created);

        if (invoice == null)
            return false;

        _context.InvoiceRows.RemoveRange(invoice.Rows);
        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ArchiveInvoiceAsync(Guid id)
    {
        var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
        if (invoice == null)
            return false;

        invoice.DeletedAt = DateTimeOffset.UtcNow;
        invoice.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<InvoiceResponseDto?> GetInvoiceByIdAsync(Guid id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

        if (invoice == null)
            return null;

        return MapToResponseDto(invoice);
    }

    public async Task<List<InvoiceResponseDto>> GetInvoicesListAsync()
    {
        var invoices = await _context.Invoices
            .Include(i => i.Rows)
            .Where(i => i.DeletedAt == null)
            .ToListAsync();

        return invoices.Select(MapToResponseDto).ToList();
    }

    private InvoiceResponseDto MapToResponseDto(Invoice invoice)
    {
        return new InvoiceResponseDto
        {
            Id = invoice.Id,
            CustomerId = invoice.CustomerId,
            StartDate = invoice.StartDate,
            EndDate = invoice.EndDate,
            Comment = invoice.Comment,
            TotalSum = invoice.TotalSum,
            Status = invoice.Status.ToString(),
            CreatedAt = invoice.CreatedAt,
            Rows = invoice.Rows.Select(r => new InvoiceRowResponseDto
            {
                Service = r.Service,
                Quantity = r.Quantity,
                Amount = r.Amount,
                Sum = r.Sum
            }).ToList()
        };
    }
}
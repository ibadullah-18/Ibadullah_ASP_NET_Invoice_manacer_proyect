using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Data;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Services;

public class InvoiceDocumentService : IInvoiceDocumentService
{
    private readonly AppDbContext _context;

    public InvoiceDocumentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<InvoiceFileDto?> DownloadInvoiceAsync(Guid invoiceId, string format)
    {
        var invoice = await _context.Invoices
            .Include(x => x.Customer)
            .Include(x => x.Rows)
            .FirstOrDefaultAsync(x => x.Id == invoiceId && x.DeletedAt == null);

        if (invoice == null)
            return null;

        format = format.Trim().ToLower();

        return format switch
        {
            "pdf" => GeneratePdf(invoice),
            "docx" => GenerateDocx(invoice),
            _ => throw new Exception("Yalnız pdf və docx formatı dəstəklənir.")
        };
    }

    private InvoiceFileDto GeneratePdf(Invoice invoice)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var fileBytes = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header()
                    .Text($"Invoice #{invoice.Id}")
                    .FontSize(20)
                    .Bold();

                page.Content().Column(col =>
                {
                    col.Spacing(8);

                    col.Item().Text($"Customer: {invoice.Customer?.Name ?? "Unknown"}");
                    col.Item().Text($"Start Date: {invoice.StartDate:dd.MM.yyyy}");
                    col.Item().Text($"End Date: {invoice.EndDate:dd.MM.yyyy}");
                    col.Item().Text($"Status: {invoice.Status}");
                    col.Item().Text($"Comment: {invoice.Comment ?? "-"}");

                    col.Item().PaddingTop(10).Text("Rows").Bold();

                    foreach (var row in invoice.Rows)
                    {
                        col.Item().Text(
                            $"Product/Service: {row.Service}, Quantity: {row.Quantity}, Unit Price: {row.Amount}, Total: {row.Sum}");
                    }

                    col.Item().PaddingTop(10).Text($"Total Sum: {invoice.TotalSum}").Bold();
                });
            });
        }).GeneratePdf();

        return new InvoiceFileDto
        {
            FileBytes = fileBytes,
            ContentType = "application/pdf",
            FileName = $"invoice-{invoice.Id}.pdf"
        };
    }

    private InvoiceFileDto GenerateDocx(Invoice invoice)
    {
        using var memoryStream = new MemoryStream();

        using (var wordDocument = WordprocessingDocument.Create(
            memoryStream,
            WordprocessingDocumentType.Document,
            true))
        {
            var mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            var body = new Body();

            body.Append(CreateParagraph($"Invoice #{invoice.Id}", true, "32"));
            body.Append(CreateParagraph($"Customer: {invoice.Customer?.Name ?? "Unknown"}"));
            body.Append(CreateParagraph($"Start Date: {invoice.StartDate:dd.MM.yyyy}"));
            body.Append(CreateParagraph($"End Date: {invoice.EndDate:dd.MM.yyyy}"));
            body.Append(CreateParagraph($"Status: {invoice.Status}"));
            body.Append(CreateParagraph($"Comment: {invoice.Comment ?? "-"}"));
            body.Append(CreateParagraph(" "));
            body.Append(CreateParagraph("Rows", true));

            foreach (var row in invoice.Rows)
            {
                body.Append(CreateParagraph(
                    $"Product/Service: {row.Service}, Quantity: {row.Quantity}, Unit Price: {row.Amount}, Total: {row.Sum}"));
            }

            body.Append(CreateParagraph(" "));
            body.Append(CreateParagraph($"Total Sum: {invoice.TotalSum}", true));

            mainPart.Document.Append(body);
            mainPart.Document.Save();
        }

        return new InvoiceFileDto
        {
            FileBytes = memoryStream.ToArray(),
            ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            FileName = $"invoice-{invoice.Id}.docx"
        };
    }

    private Paragraph CreateParagraph(string text, bool isBold = false, string fontSize = "24")
    {
        var runProperties = new RunProperties();

        if (isBold)
            runProperties.Append(new Bold());

        runProperties.Append(new FontSize { Val = fontSize });

        var run = new Run();
        run.Append(runProperties);
        run.Append(new Text(text));

        var paragraph = new Paragraph();
        paragraph.Append(run);

        return paragraph;
    }
}
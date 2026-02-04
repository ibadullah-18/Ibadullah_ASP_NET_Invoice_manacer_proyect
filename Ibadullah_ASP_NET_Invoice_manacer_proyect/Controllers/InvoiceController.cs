using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Invoice;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceDto dto)
    {
        var invoice = await _invoiceService.CreateInvoiceAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] CreateInvoiceDto dto)
    {
        try
        {
            var invoice = await _invoiceService.EditInvoiceAsync(id, dto);
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromQuery] string newStatus)
    {
        try
        {
            var result = await _invoiceService.ChangeStatusAsync(id, newStatus);
            if (!result) return NotFound("Invoice tapılmadı.");
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _invoiceService.DeleteInvoiceAsync(id);
        if (!result) return BadRequest("Invoice silinə bilmədi (yalnız Created status).");
        return NoContent();
    }

    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> Archive(Guid id)
    {
        var result = await _invoiceService.ArchiveInvoiceAsync(id);
        if (!result) return NotFound("Invoice tapılmadı.");
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
        if (invoice == null) return NotFound();
        return Ok(invoice);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {       
        var invoices = await _invoiceService.GetInvoicesListAsync();
        return Ok(invoices);
    }




}

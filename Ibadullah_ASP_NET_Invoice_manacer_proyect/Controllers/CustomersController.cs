using Ibadullah_ASP_NET_Invoice_manacer_proyect.Data;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Customer;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CustomerDto dto)
    {
        var customer = await _customerService.AddCustomerAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] CustomerDto dto)
    {
        try
        {
            var customer = await _customerService.EditCustomerAsync(id, dto);
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);
        if (!result) return BadRequest("Customer silinə bilmədi (Invoice varsa).");
        return NoContent();
    }

    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> Archive(Guid id)
    {
        var result = await _customerService.ArchiveCustomerAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var customers = await _customerService.GetCustomersListAsync();
      return Ok(customers);
    }
}


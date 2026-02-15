using Ibadullah_ASP_NET_Invoice_manacer_proyect.Data;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Customer;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerResponseDto> AddCustomerAsync(CustomerDto dto)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
      

        return MapToResponseDto(customer);
    }

    public async Task<CustomerResponseDto> EditCustomerAsync(Guid id, CustomerDto dto)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);
        if (customer == null) throw new Exception("Customer tapılmadı.");

        customer.Name = dto.Name;
        customer.Address = dto.Address;
        customer.Email = dto.Email;
        customer.PhoneNumber = dto.PhoneNumber;
        customer.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync();
        return MapToResponseDto(customer);
    }

    public async Task<bool> DeleteCustomerAsync(Guid id)
    {
        var customer = await _context.Customers.Include(c => c.Invoices)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) return false;
        if (customer.Invoices.Any()) return false; 

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ArchiveCustomerAsync(Guid id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer == null) return false;

        customer.DeletedAt = DateTimeOffset.UtcNow;
        customer.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CustomerResponseDto?> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);
        return customer == null ? null : MapToResponseDto(customer);
    }

    public async Task<IEnumerable<CustomerResponseDto>> GetCustomersListAsync()
    {
        var custamers = await _context.Customers.Where(c => c.DeletedAt == null).ToListAsync();
        return custamers.Select(MapToResponseDto);
    }

    private CustomerResponseDto MapToResponseDto(Customer customer)
    {
        return new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            CreatedAt = customer.CreatedAt
        };
    }

}

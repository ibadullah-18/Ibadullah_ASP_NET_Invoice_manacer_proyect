using Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Customer;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;

public interface ICustomerService
{
    Task<CustomerResponseDto> AddCustomerAsync(CustomerDto dto);
    Task<CustomerResponseDto> EditCustomerAsync(Guid id, CustomerDto dto);
    Task<bool> DeleteCustomerAsync(Guid id); 
    Task<bool> ArchiveCustomerAsync(Guid id); 
    Task<CustomerResponseDto?> GetCustomerByIdAsync(Guid id);
    Task<IEnumerable<CustomerResponseDto>> GetCustomersListAsync();
}

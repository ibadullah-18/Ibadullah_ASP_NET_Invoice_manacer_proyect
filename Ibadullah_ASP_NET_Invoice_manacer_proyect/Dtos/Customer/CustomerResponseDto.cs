namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Customer;

public class CustomerResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

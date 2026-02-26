namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Address { get; set; }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!; 

    public string? PhoneNumber { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
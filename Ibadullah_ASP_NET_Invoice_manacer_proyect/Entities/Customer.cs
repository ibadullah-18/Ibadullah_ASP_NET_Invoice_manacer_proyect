namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}

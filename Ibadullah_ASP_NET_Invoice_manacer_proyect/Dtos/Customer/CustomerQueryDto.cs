namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Dtos.Customer;

public class CustomerQueryDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }

    public string? OrderBy { get; set; } = "CreatedAt";
    public bool IsDescending { get; set; } = false;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

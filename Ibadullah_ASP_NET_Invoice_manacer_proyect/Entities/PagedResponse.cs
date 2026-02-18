namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Entities;

public class PagedResponse<T>
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<T> Data { get; set; }
}

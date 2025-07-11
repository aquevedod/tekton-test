namespace ProductApi.Infrastructure.ExternalServices.Models;

public class DiscountResponse
{
    public Guid ProductId { get; set; }
    public decimal Discount { get; set; }
}

namespace ProductApi.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = default!;
    public int Status { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    public decimal? Discount { get; set; }
    public decimal? FinalPrice => Discount.HasValue
        ? Price * (100 - Discount.Value) / 100
        : Price;
}

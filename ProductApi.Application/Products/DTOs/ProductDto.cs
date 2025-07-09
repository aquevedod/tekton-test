namespace ProductApi.Application.Products.Dtos;

public class ProductDto
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = default!;
    public string StatusName { get; set; } = default!;
    public int Stock { get; set; }
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalPrice { get; set; }
}

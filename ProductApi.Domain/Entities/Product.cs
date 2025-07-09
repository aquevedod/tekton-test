namespace ProductApi.Domain.Entities;

public class Product
{
    public Guid ProductId { get; set; } // Clave primaria
    public string Name { get; set; } = default!;
    public int Status { get; set; } // 0 o 1
    public int Stock { get; set; }
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    // Campos calculados o externos
    public decimal? Discount { get; set; } // % opcional desde API externa
    public decimal? FinalPrice => Discount.HasValue
        ? Price * (100 - Discount.Value) / 100
        : Price;
}

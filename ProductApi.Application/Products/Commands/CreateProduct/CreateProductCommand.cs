using MediatR;

namespace ProductApi.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public int Status { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
}

using MediatR;
using ProductApi.Application.Products.Dtos;

namespace ProductApi.Application.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<ProductDto>
{
    public Guid ProductId { get; set; }

    public GetProductByIdQuery(Guid productId)
    {
        ProductId = productId;
    }
}

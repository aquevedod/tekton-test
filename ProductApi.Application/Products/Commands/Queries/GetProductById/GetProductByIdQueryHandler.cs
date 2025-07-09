using MediatR;
using ProductApi.Application.Products.Dtos;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Constants;

namespace ProductApi.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IDiscountService _discountService;

    public GetProductByIdQueryHandler(IProductRepository repository, IDiscountService discountService)
    {
        _repository = repository;
        _discountService = discountService;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.ProductId);
        if (product == null) return null;

        var discount = await _discountService.GetDiscountByProductId(product.ProductId);

        return new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            StatusName = ProductStatuses.GetStatusName(product.Status),
            Stock = product.Stock,
            Description = product.Description,
            Price = product.Price,
            Discount = discount,
            FinalPrice = product.Price * (100 - discount) / 100
        };
    }
}

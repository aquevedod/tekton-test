using MediatR;
using ProductApi.Application.Products.Dtos;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Constants;

namespace ProductApi.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscountService _discountService;
    private readonly IProductStatusCache _statusCache;

    public GetProductByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IDiscountService discountService,
        IProductStatusCache statusCache)
    {
        _unitOfWork = unitOfWork;
        _discountService = discountService;
        _statusCache = statusCache;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
        if (product == null) return null;

        var discount = await _discountService.GetDiscountByProductId(product.ProductId);

        return new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            StatusName = _statusCache.GetStatusName(product.Status),
            Stock = product.Stock,
            Description = product.Description,
            Price = product.Price,
            Discount = discount,
            FinalPrice = product.Price * (100 - discount) / 100
        };
    }
}

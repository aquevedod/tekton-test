using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Application.Interfaces;

namespace ProductApi.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            Name = request.Name,
            Status = request.Status,
            Stock = request.Stock,
            Description = request.Description,
            Price = request.Price
        };
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.ProductId;
    }
}

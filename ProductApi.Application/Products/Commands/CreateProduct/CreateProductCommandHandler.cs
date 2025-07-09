using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Application.Interfaces;

namespace ProductApi.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
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

        await _repository.AddAsync(product);
        return product.ProductId;
    }
}

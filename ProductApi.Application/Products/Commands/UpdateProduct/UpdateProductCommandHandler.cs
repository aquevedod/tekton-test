using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
        if (existing == null)
            throw new KeyNotFoundException("Product not found.");

        existing.Name = request.Name;
        existing.Status = request.Status;
        existing.Stock = request.Stock;
        existing.Description = request.Description;
        existing.Price = request.Price;

        await _unitOfWork.Products.UpdateAsync(existing);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

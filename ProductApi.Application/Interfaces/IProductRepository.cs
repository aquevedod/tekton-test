public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task UpdateAsync(Product product);
}

public interface IDiscountService
{
    Task<decimal> GetDiscountByProductId(Guid productId);
}

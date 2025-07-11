namespace ProductApi.Application.Interfaces;

public interface IDiscountService
{
    Task<decimal> GetDiscountByProductId(Guid productId);
}

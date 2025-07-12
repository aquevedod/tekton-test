namespace ProductApi.Application.Interfaces;

public interface IDiscountService
{
    Task<decimal> GetDiscountByProductId(int productId);
}

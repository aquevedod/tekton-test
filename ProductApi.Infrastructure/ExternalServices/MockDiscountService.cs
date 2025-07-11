using ProductApi.Application.Interfaces;

namespace ProductApi.Infrastructure.ExternalServices;

public class MockDiscountService : IDiscountService
{
    private static readonly Random _random = new();

    public Task<decimal> GetDiscountByProductId(Guid productId)
    {
        // Simulamos una llamada externa con un pequeño delay
        var simulatedDelay = Task.Delay(100);
        var simulatedDiscount = _random.Next(0, 51); // 0% a 50%

        return Task.FromResult((decimal)simulatedDiscount);
    }
}

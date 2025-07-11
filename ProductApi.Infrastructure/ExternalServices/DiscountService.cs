using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.ExternalServices.Models;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace ProductApi.Infrastructure.ExternalServices;

public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;
    private readonly DiscountServiceOptions _options;

    public DiscountService(HttpClient httpClient, IOptions<DiscountServiceOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<decimal> GetDiscountByProductId(Guid productId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<DiscountResponse>($"{_options.BaseUrl}{productId}");

            return response?.Discount ?? 0;
        }
        catch (Exception ex)
        {
            // Aquí puedes loguear con Serilog si lo deseas
            return 0; // Si falla la API, se asume 0% de descuento
        }
    }
}

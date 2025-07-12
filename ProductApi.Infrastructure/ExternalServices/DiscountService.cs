using Microsoft.Extensions.Options;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.ExternalServices.Models;
using Serilog;
using System.Net.Http.Json;

namespace ProductApi.Infrastructure.ExternalServices;

public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;
    private readonly DiscountServiceOptions _options;
    private readonly ILogger _logger;

    public DiscountService(
        HttpClient httpClient,
        IOptions<DiscountServiceOptions> options,
        ILogger logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
        
        // Log para diagnosticar qué valor se está tomando
        _logger.Information("DiscountService inicializado con BaseUrl: {BaseUrl}", _options.BaseUrl);
    }

    public async Task<decimal> GetDiscountByProductId(int productId)
    {
        try
        {
            var url = $"{_options.BaseUrl}/{productId}";
            _logger.Information("Haciendo request a: {Url}", url);
            
            var response = await _httpClient.GetFromJsonAsync<DiscountResponse>(url);

            return response?.Discount ?? 0;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error al obtener descuento para el producto {ProductId} desde la API externa", productId);
            return 0;
        }
    }
}

using Microsoft.Extensions.Options;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.ExternalServices.Models;
using Serilog;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace ProductApi.Infrastructure.ExternalServices;

public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;
    private readonly DiscountServiceOptions _options;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(
        HttpClient httpClient,
        IOptions<DiscountServiceOptions> options,
        ILogger<DiscountService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
        
        _logger.LogInformation("DiscountService initialized with BaseUrl: {BaseUrl}", _options.BaseUrl);
    }

    public async Task<decimal> GetDiscountByProductId(int productId)
    {
        try
        {
            var url = $"{_options.BaseUrl}/{productId}";
            _logger.LogInformation("Making request to: {Url}", url);
            
            var response = await _httpClient.GetFromJsonAsync<DiscountResponse>(url);

            return response?.Discount ?? 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting discount for product {ProductId} from external API", productId);
            return 0;
        }
    }
}

namespace ProductApi.Infrastructure.ExternalServices;

public class DiscountServiceOptions
{
    public const string SectionName = "ExternalServices:DiscountService";
    
    public string BaseUrl { get; set; } = string.Empty;
} 
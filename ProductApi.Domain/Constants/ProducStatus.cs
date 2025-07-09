namespace ProductApi.Domain.Constants;

public static class ProductStatus
{
    public static readonly Dictionary<int, string> StatusMap = new()
    {
        { 1, "Active" },
        { 0, "Inactive" }
    };

    public static string GetStatusName(int status)
    {
        return StatusMap.TryGetValue(status, out var name)
            ? name
            : "Unknown";
    }
}

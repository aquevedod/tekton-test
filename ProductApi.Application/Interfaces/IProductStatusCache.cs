namespace ProductApi.Application.Interfaces;

public interface IProductStatusCache
{
    Dictionary<int, string> GetStatusDictionary();
    string GetStatusName(int status);
}

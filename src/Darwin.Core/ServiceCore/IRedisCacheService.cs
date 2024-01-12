namespace Darwin.Core.ServiceCore;

public interface IRedisCacheService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, DateTime? expirationCacheTime = null);

}

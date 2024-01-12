using Darwin.Core.ServiceCore;
using Darwin.Service.Configures;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Darwin.Service.Services;

public class RedisCacheService : IRedisCacheService
{
    private readonly ConnectionMultiplexer _redisConnection;
    private readonly IDatabase _database;
    private readonly RedisCacheSettings _redisCacheSettings;

    public RedisCacheService(IOptions<RedisCacheSettings> options)
    {
        _redisCacheSettings = options.Value;
        //appsettings'de abortConnect=false tanımladık bağlantı sağlanmadığında uygulamanın çalıştırılmaması anlamına gelir. 
        var connection=ConfigurationOptions.Parse(_redisCacheSettings.ConnectionString);
        _redisConnection = ConnectionMultiplexer.Connect(connection);
        _database = _redisConnection.GetDatabase();
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var value= await _database.StringGetAsync(key);
        if (value.HasValue)
            return JsonConvert.DeserializeObject<T>(value);

        return default;
    }

    public async Task SetAsync<T>(string key, T value, DateTime? expirationCacheTime = null)
    {
        TimeSpan timeUnitExpiration=expirationCacheTime.Value-DateTime.UtcNow; //Redis timespan türünden çalışır DateTime kullanmaz.Bu sebeple Convert ettik.
        await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), timeUnitExpiration);
    }
}

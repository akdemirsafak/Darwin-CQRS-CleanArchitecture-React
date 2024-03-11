namespace Darwin.Persistance.Options;

public class RedisCacheSettings
{
    public string ConnectionString { get; set; }
    public string InstanceName { get; set; } //Redis Database Name
}

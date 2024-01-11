namespace Darwin.Service.Services;

public interface ICacheableQuery //Bu interface sayesinde her req response için ayrı ayrı cache'leme işlemi yapabileceğiz.
{
    string CachingKey { get; }
    double CacheTime { get; }
}

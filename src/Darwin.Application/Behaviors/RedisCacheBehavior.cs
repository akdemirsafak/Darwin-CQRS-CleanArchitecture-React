using Darwin.Application.Services;
using MediatR;

namespace Darwin.Application.Behaviors;

public class RedisCacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{

    private readonly IRedisCacheService _redisCacheService;

    public RedisCacheBehavior(IRedisCacheService redisCacheService)
    {
        _redisCacheService = redisCacheService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is ICacheableQuery query)
        {
            var cacheKey=query.CachingKey;
            var cacheTime=query.CacheTime;

            var cachedData= await _redisCacheService.GetAsync<TResponse>(cacheKey);
            if (cachedData is not null)
                return cachedData;

            var response= await next();

            if (response is not null)
                await _redisCacheService.SetAsync(cacheKey, response, DateTime.UtcNow.AddMinutes(cacheTime));

            return response;
        }
        return await next();
    }
}

using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;

namespace Darwin.Application.Features.RedisTests
{
    public static class RedisTest
    {
        public record RedisCategoryTestCommand() : ICommand<DarwinResponse<NoContent>>;

        public class CommandHandler : ICommandHandler<RedisCategoryTestCommand, DarwinResponse<NoContent>>
        {

            private readonly IRedisTestService _redisTestService;

            public CommandHandler(IRedisTestService redisTestService)
            {
                _redisTestService = redisTestService;
            }

            public async Task<DarwinResponse<NoContent>> Handle(RedisCategoryTestCommand request, CancellationToken cancellationToken)
            {
                await _redisTestService.CreateFakeCategoryDataAsync();
                return DarwinResponse<NoContent>.Success(201);

            }
        }
    }
}

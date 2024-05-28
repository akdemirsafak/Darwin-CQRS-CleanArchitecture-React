using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Share.Dtos;

namespace Darwin.Application.Features.RedisTests;

public static class RedisTest
{
    public record RedisCategoryTestCommand() : ICommand<DarwinResponse<NoContentDto>>;

    public class CommandHandler : ICommandHandler<RedisCategoryTestCommand, DarwinResponse<NoContentDto>>
    {

        private readonly IRedisTestService _redisTestService;

        public CommandHandler(IRedisTestService redisTestService)
        {
            _redisTestService = redisTestService;
        }

        public async Task<DarwinResponse<NoContentDto>> Handle(RedisCategoryTestCommand request, CancellationToken cancellationToken)
        {
            await _redisTestService.CreateFakeCategoryDataAsync();
            return DarwinResponse<NoContentDto>.Success(201);

        }
    }
}

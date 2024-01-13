using Bogus;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Infrastructure.DbContexts;
using Darwin.Model.Common;
using Darwin.Service.Common;
using Darwin.Service.Helper;

namespace Darwin.Service.RedisTest;

public static class RedisTest
{
    public record RedisCategoryTestCommand() : ICommand<DarwinResponse<NoContent>>;
    
    public class CommandHandler:ICommandHandler<RedisCategoryTestCommand,DarwinResponse<NoContent>>
    {
        private readonly DarwinDbContext _dbContext;
        private readonly ICurrentUser _currentUser;
        
        public CommandHandler(DarwinDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task<DarwinResponse<NoContent>> Handle(RedisCategoryTestCommand request, CancellationToken cancellationToken)
        {
            var categoryFaker= new Faker<Category>("tr")//Optional paramter
                .RuleFor(a=>a.Name,f=>f.Company.CompanyName())
                .RuleFor(a=>a.ImageUrl,f=>f.Image.LoremPixelUrl())
                .RuleFor(a=>a.IsUsable,true)
                .RuleFor(a=>a.CreatedBy,_currentUser.GetUserId)
                .RuleFor(a=>a.CreatedOnUtc,DateTime.UtcNow);
            var generatedObject=categoryFaker.Generate(1000000); //Optional Parameter
            
            await _dbContext.AddRangeAsync(generatedObject);
            //await _dbContext.SaveChangesAsync();
            return DarwinResponse<NoContent>.Success(201);

        }
    }
}
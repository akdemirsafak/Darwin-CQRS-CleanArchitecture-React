using Bogus;
using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Persistance.DbContexts;

namespace Darwin.Persistance.Services;

public sealed class RedisTestService : IRedisTestService
{
    private readonly DarwinDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public RedisTestService(DarwinDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task CreateFakeCategoryDataAsync()
    {
        var categoryFaker= new Faker<Category>("tr")//Optional paramter
                .RuleFor(a=>a.Name,f=>f.Company.CompanyName())
                .RuleFor(a=>a.ImageUrl,f=>f.Image.LoremPixelUrl())
                .RuleFor(a=>a.IsUsable,true)
                .RuleFor(a=>a.CreatedBy,_currentUser.GetUserId)
                .RuleFor(a=>a.CreatedOnUtc,DateTime.UtcNow);
        var generatedObject=categoryFaker.Generate(100); //Optional Parameter 100

        await _dbContext.AddRangeAsync(generatedObject);
        //await _dbContext.SaveChangesAsync(); // UnitOfWork Behavior kullandığımız için iptal ettik.

    }
}

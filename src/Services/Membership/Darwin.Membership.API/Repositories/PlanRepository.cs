using Darwin.Membership.API.DbContexts;
using Darwin.Membership.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Membership.API.Repositories;

public class PlanRepository : IPlanRepository
{
    private readonly MembershipDbContext _dbContext;

    public PlanRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Plan> CreateAsync(Plan entity)
    {
        await _dbContext.Plans.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public Task DeleteAsync(Plan entity)
    {
        _dbContext.Plans.Remove(entity);
        return _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Plan>> GetAllAsync()
    {
        return await _dbContext.Plans.ToListAsync();
    }

    public async Task<Plan> GetByIdAsync(Guid id)
    {
        return await _dbContext.Plans.FindAsync(id);
    }

    public async Task UpdateAsync(Plan entity)
    {
        _dbContext.Plans.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}

using Darwin.Membership.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Membership.API.DbContexts;

public class MembershipDbContext : DbContext
{

    public MembershipDbContext(DbContextOptions<MembershipDbContext> options) : base(options)
    {
    }
    public DbSet<Plan> Plans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

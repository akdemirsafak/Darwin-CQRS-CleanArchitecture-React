using Darwin.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Infrastructure.DbContexts;

public class DarwinDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public DarwinDbContext(DbContextOptions<DarwinDbContext> options) : base(options)
    {

    }
    public DbSet<Music> Musics { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<AgeRate> AgeRates { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Music>().Navigation(c => c.Moods).AutoInclude();
        builder.Entity<Music>().Navigation(c => c.Categories).AutoInclude();
        builder.Entity<Music>().Navigation(c => c.AgeRate).AutoInclude();
        base.OnModelCreating(builder);
    }
}

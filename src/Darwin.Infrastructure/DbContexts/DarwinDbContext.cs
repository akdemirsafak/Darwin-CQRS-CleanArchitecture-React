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
    public DbSet<ContentAgeRate> ContentAgeRates { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Mood>().Navigation(c => c.Music).AutoInclude();
        builder.Entity<Category>().Navigation(c => c.Musics).AutoInclude();
        builder.Entity<Music>().Navigation(c => c.ContentAgeRate).AutoInclude();
        base.OnModelCreating(builder);
    }
}

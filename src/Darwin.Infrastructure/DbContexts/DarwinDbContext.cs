using Darwin.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Infrastructure.DbContexts;

public class DarwinDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public DarwinDbContext(DbContextOptions<DarwinDbContext> options) : base(options)
    {

    }
    public DbSet<Content> Contents { get; set; }
    public DbSet<PlayList> PlayLists { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<PlayList>().Navigation(c => c.Contents).AutoInclude();
        builder.Entity<Content>().Navigation(x => x.Categories).AutoInclude();
        builder.Entity<Content>().Navigation(x => x.Moods).AutoInclude();

        base.OnModelCreating(builder);
    }
}

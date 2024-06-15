using Darwin.Contents.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Darwin.Contents.Repository.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Mood> Moods { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<Content>().Navigation(x => x.Categories).AutoInclude();
        builder.Entity<Content>().Navigation(x => x.Moods).AutoInclude();
        base.OnModelCreating(builder);
    }
}

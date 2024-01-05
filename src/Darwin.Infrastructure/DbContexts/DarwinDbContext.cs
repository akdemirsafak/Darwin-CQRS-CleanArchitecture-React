using Darwin.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<PlayList>().Navigation(c => c.Contents).AutoInclude();
        builder.Entity<Content>().Navigation(x => x.Categories).AutoInclude();
        builder.Entity<Content>().Navigation(x => x.Moods).AutoInclude();

        base.OnModelCreating(builder);
    }
    StreamWriter _log=new("../Darwin.Infrastructure/entityFrameworkLogs/logs.txt",append:true);
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.LogTo(async message=>await _log.WriteLineAsync(message),LogLevel.Warning)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
    public override void Dispose()
    {
        base.Dispose();
        _log.Dispose();
    }
    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _log.DisposeAsync();
    }
}

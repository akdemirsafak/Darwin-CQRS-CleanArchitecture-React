using System.Security.Cryptography.X509Certificates;
using Darwin.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Infrastructure;

public class DarwinDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public DarwinDbContext(DbContextOptions<DarwinDbContext> options) : base(options)
    {

    }
    public DbSet<Music> Musics { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<MusicCategory> MusicCategories { get; set; }
    public DbSet<MusicMood> MusicMoods { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

using Darwin.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Infrastructure;

public class DarwinDbContext:DbContext
{
    public DarwinDbContext(DbContextOptions<DarwinDbContext> options) : base(options)
    {
        
    }
    public DbSet<Music> Musics { get; set; }
}

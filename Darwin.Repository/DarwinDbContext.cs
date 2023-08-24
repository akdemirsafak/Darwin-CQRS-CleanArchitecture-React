using Darwin.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Repository;

public class DarwinDbContext:DbContext
{
    public DarwinDbContext(DbContextOptions<DarwinDbContext> options) : base(options)
    {
        
    }
    public DbSet<Music> Musics { get; set; }

}

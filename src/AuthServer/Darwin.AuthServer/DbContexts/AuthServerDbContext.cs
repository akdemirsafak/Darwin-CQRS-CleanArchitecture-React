using Darwin.AuthServer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Darwin.AuthServer.DbContexts;

public class AuthServerDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AuthServerDbContext(DbContextOptions<AuthServerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

}

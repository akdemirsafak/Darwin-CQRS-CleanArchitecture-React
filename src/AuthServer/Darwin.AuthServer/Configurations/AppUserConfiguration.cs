using Darwin.AuthServer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Darwin.AuthServer.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(prop => prop.Name).HasMaxLength(16);
        builder.Property(prop => prop.LastName).HasMaxLength(16);
        builder.Property(prop => prop.CreatedBy).HasDefaultValue(DateTime.Now);
    }
}

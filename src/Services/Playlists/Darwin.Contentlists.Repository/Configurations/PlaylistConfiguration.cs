using Darwin.Contentlists.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Darwin.Contentlists.Repository.Configurations;

public sealed class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(32);
        builder.Property(x => x.Description).HasMaxLength(256);
        builder.Property(x=> x.IsPublic).HasDefaultValue(false);
    }
}

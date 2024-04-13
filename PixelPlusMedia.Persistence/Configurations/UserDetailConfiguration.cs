using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Persistence.Configurations;

public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        builder.Property(e => e.MediaUrl)
        .IsRequired();

        builder.Property(e => e.DefaultMessage)
        .HasMaxLength(50);

        builder.Property(e => e.Message)
        .HasMaxLength(50);

        builder.Property(e => e.IsApprove)
        .HasDefaultValue(false);

        builder.Property(e => e.IsActive)
        .HasDefaultValue(true);

        builder.Property(e => e.DateRegistered)
        .IsRequired();
    }
}

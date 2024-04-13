using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Persistence.Configurations
{
    public class SubMessageConfiguration : IEntityTypeConfiguration<SubMessage>
    {
        public void Configure(EntityTypeBuilder<SubMessage> builder)
        {
            builder.Property(e => e.Top)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(e => e.Right)
                .HasColumnType("text");

            builder.Property(e => e.Bottom)
                .HasColumnType("text");

            builder.Property(e => e.Left)
                .HasColumnType("text");

            builder.Property(e => e.ContentId)
                .IsRequired();
        }
    }
}

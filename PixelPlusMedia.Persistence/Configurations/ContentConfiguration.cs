using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelPlusMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlusMedia.Persistence.Configurations
{
    public class ContentConfiguration: IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.Property(e => e.WelcomeMessage)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(e => e.ThankyouMessage)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(e => e.Tnc)
                .HasColumnType("text")
                .IsRequired();
        }
    }
}

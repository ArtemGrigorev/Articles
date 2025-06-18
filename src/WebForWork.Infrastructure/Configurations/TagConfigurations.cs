using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Infrastructure.Configurations
{

    public class TagConfigurations : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("tags");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasConversion(id => id.Value,
                value => new TagId(value))
                .IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        }
    }
}

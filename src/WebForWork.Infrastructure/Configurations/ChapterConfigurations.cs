using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Infrastructure.Configurations
{
    public class ChapterConfigurations : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable("chapters");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("id")
                   .HasConversion(id => id.Value,
                    value => new ChapterId(value))
                   .IsRequired();
            builder.Property(x => x.Name)
                   .HasColumnName("name")
                   .HasConversion(name => name.Value,
                    value => new ChapterName(value))
                   .IsRequired()
                   .HasMaxLength(1024);
        }
    }
}

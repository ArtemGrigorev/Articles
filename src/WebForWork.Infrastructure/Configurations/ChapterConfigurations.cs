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

            builder.HasMany(e => e.Tags)
                   .WithMany(e => e.Chapters)
                   .UsingEntity(
                   "chapter_tag",
                   r => r.HasOne(typeof(Tag)).WithMany().HasForeignKey("tagId").HasPrincipalKey(nameof(Tag.Id)),
                   l => l.HasOne(typeof(Chapter)).WithMany().HasForeignKey("articleId").HasPrincipalKey(nameof(Chapter.Id)),
                   j => j.HasKey("chapterId", "tagId"));

        }
    }
}

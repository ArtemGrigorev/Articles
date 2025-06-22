using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.Entities;

namespace WebForWork.Infrastructure.Configurations
{
    public class ChapterTagConfigurations : IEntityTypeConfiguration<ChapterTag>
    {
        public void Configure(EntityTypeBuilder<ChapterTag> builder)
        {
            builder.ToTable("chapters_tags");
            builder.Property(x => x.order)
                   .HasColumnName("order");
            builder.HasKey("tagId", "chapterId");
            builder.HasOne(x => x.chapter).WithMany(x => x.Tags).HasForeignKey("chapterId");
            builder.HasOne(x => x.tag).WithMany(x => x.Chapters).HasForeignKey("tagId");
        }
    }
}

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
            builder.HasKey("tagId", "chapterId");
            builder.HasOne(typeof(Tag)).WithMany().HasForeignKey("tagId").HasPrincipalKey(nameof(Tag.Id));
            builder.HasOne(typeof(Chapter)).WithMany().HasForeignKey("chapterId").HasPrincipalKey(nameof(Chapter.Id));
        }
    }
}

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
    public class ArticleConfigurations : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("articles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasConversion(id => id.Value,
                value => new ArticleId(value))
                .IsRequired();
            builder.Property(x => x.Name)
               .HasColumnName("name")
               .HasConversion(name => name.Value,
               value => new ArticleName(value))
               .IsRequired()
               .HasMaxLength(256);
            builder.HasMany(e => e.Tags)
                   .WithMany(e => e.Articles)
                   .UsingEntity(
                   "article_tag",
                   t => t.HasOne(typeof(Tag)).WithMany().HasForeignKey("tagId").HasPrincipalKey(nameof(Tag.Id)),
                   a => a.HasOne(typeof(Article)).WithMany().HasForeignKey("articleId").HasPrincipalKey(nameof(Article.Id)),
                   ta => ta.HasKey("tagId", "articleId"));
            builder.Property(x => x.CreateDate).IsRequired().HasColumnName("create_date");
            builder.Property(x => x.UpdateDate).HasColumnName("update_date");
        }
    }
}

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
    public class ArticleTagConfigurations : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.ToTable("articles_tags");
           /* builder.Property(x => x.order)
                   .HasColumnName("order");*/
            builder.HasKey("tagId", "articleId");
            //  builder.HasOne(typeof(Article)).WithMany("ArticleTags").HasForeignKey("articleId").HasPrincipalKey(nameof(Article.Id)).IsRequired();
            //  builder.HasOne(typeof(Tag)).WithMany("ArticleTags").HasForeignKey("tagId").HasPrincipalKey(nameof(Tag.Id)).IsRequired();

            builder.HasOne(x => x.article).WithMany(x => x.Tags).HasForeignKey("articleId");//.HasPrincipalKey(nameof(ArticleId.Value));//.HasForeignKey(x => x.articleId.Value);//.HasPrincipalKey(nameof(Article.Id)).IsRequired();
            builder.HasOne(x => x.tag).WithMany(x => x.Articles).HasForeignKey("tagId");//.HasPrincipalKey(nameof(Tag.Id)).IsRequired();
        }
    }
}

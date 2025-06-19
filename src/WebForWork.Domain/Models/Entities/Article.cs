using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Exceptions;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public sealed class Article : Entity<ArticleId, ArticleName>
    {
        const int invariantCountMaxTags = 256;
        const int invariantLengthMaxName = 256;

        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }

        public List<Tag> Tags { get; private set; } = new();

        private Article(ArticleId id, ArticleName name) : base(id, name)
        {

        }

        public static Article Create(IEnumerable<string> tags, string name, TimeProvider timeProvider)
        {
            if (tags.Count() > invariantCountMaxTags)
            {
                throw new ArticleTagsException($"Количество тегов превышает допустимое значение = {invariantCountMaxTags}");
            }

            if (name.Length > invariantLengthMaxName)
            {
                throw new ArticleNameException($"Длина имени превышает допустимое значение = {invariantCountMaxTags}");
            }
            Article article = new Article(new ArticleId(Guid.NewGuid()), new ArticleName(name));
            article.Tags = tags.Select(x => new Tag(new TagId(Guid.NewGuid()), new TagName(name))).ToList();
            article.CreateDate = timeProvider.GetLocalNow().UtcDateTime;

            return article;
        }
    }
}

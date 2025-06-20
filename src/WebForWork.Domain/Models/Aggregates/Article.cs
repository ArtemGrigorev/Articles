using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Exceptions;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Aggregates
{
    public sealed class Article : Aggregate<ArticleId, ArticleName>
    {
        public const int invariantCountMaxTags = 256;
        public const int invariantLengthMaxName = 256;

        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }

       // public List<Tag> Tags { get; private set; } = new();
        public List<ArticleTag> Tags { get; private set; } = new();

        private Article(ArticleId id, ArticleName name) : base(id, name)
        {

        }

        public static Article Create(IList<Tag> tags, IEnumerable<string> tagsNames, string name, TimeProvider timeProvider)
        {
            if (tags.Count() > invariantCountMaxTags)
            {
                throw new ArticleTagsException($"Количество тегов превышает допустимое значение ValueMax = {invariantCountMaxTags}");
            }

            if (name.Length > invariantLengthMaxName)
            {
                throw new ArticleNameException($"Длина имени превышает допустимое значение ValueMax = {invariantLengthMaxName}");
            }

            var exceptTags = tags.Select(t => t.Name.Value).ToList();
            var noExceptTags = tagsNames.Except(exceptTags);
            var newTags = noExceptTags.Select(x => Tag.Create(x)).ToList();
            var sumTags = newTags.Union(tags);
            Article article = new Article(new ArticleId(Guid.NewGuid()), new ArticleName(name));
            article.Tags = sumTags.Select(x => new ArticleTag() { article = article, tag = x }).ToList();
            //(new TagId(Guid.NewGuid()), new TagName(name))).ToList();
            article.CreateDate = timeProvider.GetLocalNow().UtcDateTime;

            return article;
        }
    }
}

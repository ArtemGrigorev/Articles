using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        public List<ArticleTag> Tags { get; private set; } = new();

        private Article(ArticleId id, ArticleName name) : base(id, name)
        {

        }

        private static void InvariantValidate(IList<Tag> tags, IEnumerable<string> tagsNames, string name)
        {
            if (tagsNames.Count() > invariantCountMaxTags)
            {
                throw new ArticleTagsException($"Количество тегов превышает допустимое значение ValueMax = {invariantCountMaxTags}");
            }

            if (name.Length > invariantLengthMaxName)
            {
                throw new ArticleNameException($"Длина имени превышает допустимое значение ValueMax = {invariantLengthMaxName}");
            }

            if (tagsNames.Distinct().Count() != tagsNames.Count())
            {
                throw new ArticleUniqueTagException("Значения тегов в статье не уникально");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="tagsNames">Значение тегов в запросе</param>
        /// <param name="name"></param>
        /// <param name="timeProvider"></param>
        /// <returns></returns>
        /// <exception cref="ArticleTagsException"></exception>
        /// <exception cref="ArticleNameException"></exception>
        /// <exception cref="ArticleUniqueTagException"></exception>
        public static Article Create(IList<Tag> tags, IEnumerable<string> tagsNames, string name, TimeProvider timeProvider)
        {
            InvariantValidate(tags,tagsNames,name);
            var existsTags = tags.Select(t => t.Name.Value).ToList();
            var noExistsTags = tagsNames.Except(existsTags);
            var newTags = noExistsTags.Select(x => Tag.Create(x)).ToList();
            var sumTags = newTags.Union(tags);
            sumTags = sumTags.OrderBy(st => tagsNames.ToList().IndexOf(st.Name.Value)).ToList();

            Article article = new Article(new ArticleId(Guid.NewGuid()), new ArticleName(name));
            article.Tags = sumTags.Select((tag, index) => new ArticleTag()
            {
                article = article,
                tag = newTags.Contains(tag) ? tag : null,
                tagId = tag.Id,
                order = index
            }).ToList();
            article.CreateDate = timeProvider.GetLocalNow().UtcDateTime;
            return article;
        }

        public void Update(IList<Tag> tags, IEnumerable<string> tagsNames, string name, TimeProvider timeProvider)
        {
            InvariantValidate(tags, tagsNames, name);
            var existsTags = tags.Select(t => t.Name.Value).ToList();
            var noExistsTags = tagsNames.Except(existsTags);
            var newTags = noExistsTags.Select(x => Tag.Create(x)).ToList();
            var sumTags = newTags.Union(tags);
            sumTags = sumTags.OrderBy(st => tagsNames.ToList().IndexOf(st.Name.Value)).ToList();
            Tags = sumTags.Select((tag, index) => new ArticleTag()
            {
                tag = newTags.Contains(tag) ? tag : null,
                tagId = tag.Id,
                order = index
            }).ToList();
            UpdateDate = timeProvider.GetLocalNow().UtcDateTime;
        }
    }
}

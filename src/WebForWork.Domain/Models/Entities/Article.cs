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
    public class Article: BaseEntity<ArticleId,ArticleName>
    {
        const int invariantCountMaxTags = 256;
        const int invariantLengthMaxName = 256;

        private readonly TimeProvider _timeProvider;
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }

        private List<Tag> Tags { get; set; } = new();

        public Article(ArticleId articleId, ArticleName articleName, TimeProvider timeProvider) : base(articleId, articleName)
        { 
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
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
            Article article = new Article(new ArticleId(Guid.NewGuid()), new ArticleName(name), timeProvider);
            article.Tags = tags.Select(x => new Tag(new TagId(Guid.NewGuid()), name)).ToList();
            article.CreateDate = DateTime.Now;

            return article;
        }
    }
}

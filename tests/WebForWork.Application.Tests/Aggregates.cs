using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Exceptions;
using System.Collections.ObjectModel;

namespace WebForWork.Application.Tests
{
    public class Aggregates
    {

        [Fact]
        public void MustBeExceptionTagNameException()
        {
          Action action = () => Tag.Create("");
          Assert.Throws<TagNameException>(() => action());
        }

        [Fact]
        public void MustBeExceptionArticleTagsException()
        {
            var tagsName = new Collection<string>();
            for (int i = 0; i <= Article.invariantCountMaxTags; i++)
            {
                tagsName.Add(i.ToString());
            }
            Action action = () => Article.Create(new List<Tag>(), tagsName, string.Empty, TimeProvider.System);
            Assert.Throws<ArticleTagsException>(() => action());
        }
    }
}
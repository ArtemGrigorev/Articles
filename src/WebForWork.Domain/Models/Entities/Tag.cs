using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Exceptions;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public class Tag : BaseEntity<TagId,TagName>
    {
        public const int invariantLengthMaxName = 256;
        public Tag(TagId id, TagName name) : base(id, name)
        {
        }

        public List<ArticleTag> Articles { get; private set; } = new();
        // public List<Article> Articles { get; } = new();
        public List<ChapterTag> Chapters { get; private set; } = new();
        //  public List<Chapter> Chapters { get; } = new();


        public static Tag Create(string name)
        {
            if (name.Length > invariantLengthMaxName)
            {
                throw new TagNameException($"Длина имени = {name} превышает допустимое значение ValueMax = {invariantLengthMaxName}");
            }
            Tag tag = new Tag(new TagId(Guid.NewGuid()), new TagName(name));

            return tag;
        }
    }
}

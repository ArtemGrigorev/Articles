using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Exceptions;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Aggregates
{
    public sealed class Chapter : Aggregate<ChapterId, ChapterName>
    {
        private const int invariantLengthMaxName = 1024;
        public List<ChapterTag> Tags { get; private set; } = new();
        private Chapter(ChapterId id, ChapterName name) : base(id, name)
        {

        }

        public static Chapter Create(IEnumerable<Tag> tags)
        {
            Chapter chapter = null;
            try
            {
                string name = string.Join(", ", tags.Select(x => x.Name.Value).ToArray());
                if (name.Length > invariantLengthMaxName)
                    name = name.Substring(0, invariantLengthMaxName);
                chapter = new Chapter(new ChapterId(Guid.NewGuid()), new ChapterName(name));
                chapter.Tags = tags.Select((tag, index) => new ChapterTag()
                {
                    chapter = chapter,
                    tagId = tag.Id,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new АggregateCreateException("Ошибка создания раздела", ex);
            }

            return chapter;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public sealed class Chapter: Entity<ChapterId, ChapterName>
    {
        private Chapter(ChapterId id, ChapterName name) : base(id, name)
        {
        }

        public List<Tag> Tags { get; } = new();
    }
}

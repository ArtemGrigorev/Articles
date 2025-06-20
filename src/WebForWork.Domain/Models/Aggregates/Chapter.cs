using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Aggregates
{
    public sealed class Chapter: Aggregate<ChapterId, ChapterName>
    {
        public List<ChapterTag> ChapterTags { get; private set; } = new();
       // public List<Tag> Tags { get; private set; } = new();
        private Chapter(ChapterId id, ChapterName name) : base(id, name)
        {

        }

   
    }
}

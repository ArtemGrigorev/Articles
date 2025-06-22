using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public class ChapterTag
    {
        public ChapterId chapterId { get; set; }
        public Chapter chapter { get; set; }
        public TagId tagId { get; set; }
        public Tag tag { get; set; }
        public int order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public class Chapter: BaseEntity<ChapterId>
    {
        public List<Tag> Tags { get; } = new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public class Tag : BaseEntity<TagId>
    {
        public List<Article> Articles { get; } = new();
        public List<Chapter> Chapters { get; } = new();
    }
}

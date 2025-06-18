using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public class Article: BaseEntity<ArticleId>
    {
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }

        public List<Tag> Tags { get; } = new();
    }
}

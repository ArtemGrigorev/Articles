using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public class ArticleTag
    {
        public ArticleId articleId { get; set; }
        public Article article { get; set; }
        public TagId tagId { get; set; }
        public Tag tag { get; set; }
        //public int order { get; set; }      
    }
}

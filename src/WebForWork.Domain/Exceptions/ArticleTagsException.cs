using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Domain.Exceptions
{
    public class ArticleTagsException : DomainException
    {
        public ArticleTagsException(string message) : base(message) 
        { }
    }
}

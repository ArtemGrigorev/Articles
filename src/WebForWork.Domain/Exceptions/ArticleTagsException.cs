using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Domain.Exceptions
{
    internal class ArticleTagsException : DomainException
    {
        internal ArticleTagsException(string message) : base(message) 
        { }
    }
}

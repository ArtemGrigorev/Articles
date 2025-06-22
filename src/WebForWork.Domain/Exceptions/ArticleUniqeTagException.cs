using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Domain.Exceptions
{
    internal class ArticleUniqueTagException: DomainException
    {
        internal ArticleUniqueTagException(string message) : base(message)
        { }
    }
}

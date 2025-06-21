using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Domain.Exceptions
{
    internal class АggregateCreateException : DomainException
    {
        internal АggregateCreateException(string message) : base(message)
        {
        }

        internal АggregateCreateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

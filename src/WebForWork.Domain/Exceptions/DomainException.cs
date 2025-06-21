using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        internal protected DomainException() { }

        internal protected DomainException(string message):base(message) 
        {
        }

        internal protected DomainException(string message, Exception inner) : base(message, inner)
        {
        }

    }
}

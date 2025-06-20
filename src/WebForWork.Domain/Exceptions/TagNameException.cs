using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Domain.Exceptions
{
    internal class TagNameException : DomainException
    {
        internal TagNameException(string message) : base(message)
        { 
        }
    }
   
}

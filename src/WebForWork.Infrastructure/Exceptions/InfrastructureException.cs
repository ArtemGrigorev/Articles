using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Infrastructure.Exceptions
{
    internal class InfrastructureException : Exception 
    {
        internal protected InfrastructureException() { }

        internal protected InfrastructureException(string messagу) 
        {
        }
        internal protected InfrastructureException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

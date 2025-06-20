using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Infrastructure.Exceptions
{
    internal class TagRepositoriesException : InfrastructureException
    {
        internal TagRepositoriesException(string message, Exception inner) : base(message, inner)
        { 
        }
    }
}

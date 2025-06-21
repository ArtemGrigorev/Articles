using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Infrastructure.Exceptions
{
    internal class ChapterRepositoriesException : InfrastructureException
    {
        internal ChapterRepositoriesException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

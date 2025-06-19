using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands
{
    public class CreateArticleCommandResult
    {
        public Guid ID { get; set; }

        public string MessageError { get; set; }
    }
}

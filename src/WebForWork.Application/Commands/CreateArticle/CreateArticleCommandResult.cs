using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands.CreateArticle
{
    public class CreateArticleCommandResult
    {
        public Guid Id { get; set; }
        public string MessageError { get; set; } = string.Empty;
    }
}

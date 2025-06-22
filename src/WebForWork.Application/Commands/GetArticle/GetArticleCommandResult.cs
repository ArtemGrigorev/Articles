using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands.GetArticle
{
    public class GetArticleCommandResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<string> Tags { get; set; }

        public string MessageError { get; set; } = string.Empty;
    }
}

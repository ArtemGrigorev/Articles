using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands.CreateArticle
{
    public class CreateArticleCommand : IRequest<CreateArticleCommandResult>
    {
        public string Name  { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}

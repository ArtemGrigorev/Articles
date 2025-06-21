using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands
{
    public class GetArticleCommand : IRequest<GetArticleCommandResult>
    {
        public Guid Id { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands
{
    public class UpdateArticleCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}

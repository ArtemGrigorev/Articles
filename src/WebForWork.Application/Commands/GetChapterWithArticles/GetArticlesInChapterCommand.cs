using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands.GetChapterWithArticles
{
    public class GetArticlesInChapterCommand : IRequest<GetArticlesInChapterResult>
    {
        public Guid Id { get; set; }
    }
}

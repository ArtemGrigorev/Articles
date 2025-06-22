using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Application.Commands.GetChapterWithArticles;

namespace WebForWork.Application.Commands.GetChaptersCatalog
{
    public class GetChaptersCatalogCommand : IRequest<GetChaptersCatalogResult>
    {
        public byte Page { get; set; }
    }
}

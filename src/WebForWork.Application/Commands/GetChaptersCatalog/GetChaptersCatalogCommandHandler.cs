using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Application.Commands.GetChapterWithArticles;
using WebForWork.Domain.Events;
using WebForWork.Domain.Models.ValueObject;
using WebForWork.Domain.Repositories;

namespace WebForWork.Application.Commands.GetChaptersCatalog
{
    public class GetChaptersCatalogCommandHandler : IRequestHandler<GetChaptersCatalogCommand, GetChaptersCatalogResult>
    {
        private readonly IChapterRepositories _chapterRepositories;

        public GetChaptersCatalogCommandHandler(IChapterRepositories chapterRepositories
        )
        {
            _chapterRepositories = chapterRepositories ?? throw new ArgumentNullException(nameof(chapterRepositories));
        }
        public async Task<GetChaptersCatalogResult> Handle(GetChaptersCatalogCommand request, CancellationToken cancellationToken)
        {
            var result = new GetChaptersCatalogResult();
            try
            {
                var catalog = _chapterRepositories.GetChaptersAsNoTracking(request.Page);
                                                

                result.Attributes = catalog.Select(c =>
                    new AttributesApplicationDTO()
                    {
                        Id = c.Id,
                        Name = c.Name
                    }
                ).ToList();
            }
            catch (Exception ex)
            {
                result.MessageError = ex.Message;
            }

            return result;
        }
    }
}

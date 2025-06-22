using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.ValueObject;
using WebForWork.Domain.Repositories;

namespace WebForWork.Application.Commands.GetChapterWithArticles
{
    public class GetArticlesInChapterCommandHandler : IRequestHandler<GetArticlesInChapterCommand, GetArticlesInChapterResult>
    {
        private readonly IChapterRepositories _chapterRepositories;
        private readonly IArticleRepositories _articleRepositories;

        public GetArticlesInChapterCommandHandler(IChapterRepositories chapterRepositories,
            IArticleRepositories articleRepositories)
        {
            _chapterRepositories = chapterRepositories ?? throw new ArgumentNullException(nameof(chapterRepositories));
            _articleRepositories = articleRepositories ?? throw new ArgumentNullException(nameof(articleRepositories));
        }

        public async Task<GetArticlesInChapterResult> Handle(GetArticlesInChapterCommand request, CancellationToken cancellationToken)
        {
            var result = new GetArticlesInChapterResult();
            try
            {
                var chapterId = new ChapterId(request.Id);
                var chapter = await _chapterRepositories.GetChapterAsNoTrackingAsync(chapterId, cancellationToken);
                var tags = chapter.Tags.Select(x => x.tag);
                var articles = await _articleRepositories.GetArticlesByTagsAsync(tags, cancellationToken);
                articles = articles.OrderByDescending(x => x.UpdateDate != DateTime.MinValue ? x.UpdateDate : x.CreateDate).ToList();
                result.Id = chapterId.Value;
                result.Name = chapter.Name.Value;
                result.Attributes = articles.Select(a =>
                    new AttributesApplicationDTO()
                    {
                        Id = a.Id.Value,
                        Name = a.Name.Value
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


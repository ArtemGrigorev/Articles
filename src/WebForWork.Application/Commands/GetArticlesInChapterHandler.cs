using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.ValueObject;
using WebForWork.Domain.Repositories;

namespace WebForWork.Application.Commands
{
    public class GetArticlesInChapterHandler : IRequestHandler<GetArticlesInChapterCommand, GetArticlesInChapterResult>
    {
        private readonly IChapterRepositories _chapterRepositories;
        private readonly IArticleRepositories _articleRepositories;

        public GetArticlesInChapterHandler(IChapterRepositories chapterRepositories, 
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

              //  var t = articles.OrderByDescending(x => (x.UpdateDate ?? x.CreateDate));

        /*    result.Id = articleId.Value;
            result.Name = article.Name.Value;
            result.Tags = sortTags;*/
    }
            catch (Exception ex)
            {
                result.MessageError = ex.Message;
            }

            return result;
        }
    }
    }
}

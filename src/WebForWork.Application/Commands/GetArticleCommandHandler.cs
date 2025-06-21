using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.ValueObject;
using WebForWork.Domain.Repositories;

namespace WebForWork.Application.Commands
{
    public class GetArticleCommandHandler : IRequestHandler<GetArticleCommand, GetArticleCommandResult>
    {
        private readonly IArticleRepositories _articleRepositories;

        public GetArticleCommandHandler(IArticleRepositories articleRepositories)
        {
            _articleRepositories = articleRepositories ?? throw new ArgumentNullException(nameof(articleRepositories));
        }
        public async Task<GetArticleCommandResult> Handle(GetArticleCommand request, CancellationToken cancellationToken)
        {
            var result = new GetArticleCommandResult();
            try
            {
                var articleId = new ArticleId(request.Id);
                var article = await _articleRepositories.GetArticleAsNoTrackingAsync(articleId, cancellationToken);

                var sortTags = article.Tags
                              .OrderBy(x => x.order)
                              .Select(x => x.tag.Name.Value);

                result.Id = articleId.Value;
                result.Name = article.Name.Value;
                result.Tags = sortTags;
            }
            catch (Exception ex)
            {
                result.MessageError = ex.Message;
            }

            return result;
        }
    }
}

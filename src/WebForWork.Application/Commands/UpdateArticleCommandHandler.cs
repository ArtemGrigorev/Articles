using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Events;
using WebForWork.Domain.Models.ValueObject;
using WebForWork.Domain.Repositories;

namespace WebForWork.Application.Commands
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
    {
        private readonly IArticleRepositories _articleRepositories;
        private readonly ITagRepositories _tagRepositories;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        public UpdateArticleCommandHandler(TimeProvider timeProvider,
                       IArticleRepositories articleRepositories,
                       ITagRepositories tagRepositories,
                       IUnitOfWork unitOfWork)
        {
            _articleRepositories = articleRepositories ?? throw new ArgumentNullException(nameof(articleRepositories));
            _tagRepositories = tagRepositories ?? throw new ArgumentNullException(nameof(tagRepositories));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        }
        public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tagNames = request.Tags.Select(x => new TagName(x));
                var tags = await _tagRepositories.GetTagsByNamesAsync(tagNames, cancellationToken);
                var articleId = new ArticleId(request.Id);
                var article = await _articleRepositories.GetArticleAsync(articleId, cancellationToken);
                article.Update(tags, request.Tags, request.Name, _timeProvider);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

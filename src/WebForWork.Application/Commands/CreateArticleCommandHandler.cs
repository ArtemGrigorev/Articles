using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Events;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Repositories;

namespace WebForWork.Application.Commands
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreateArticleCommandResult>
    {
        private readonly TimeProvider _timeProvider;
        private readonly IArticleRepositories _articleRepositories;
        private readonly ITagRepositories _tagRepositories;
        private readonly IUnitOfWork _unitOfWork;
        public CreateArticleCommandHandler(TimeProvider timeProvider, 
            IArticleRepositories articleRepositories,
            ITagRepositories tagRepositories,
            IUnitOfWork unitOfWork) 
        {
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
            _articleRepositories = articleRepositories ?? throw new ArgumentNullException(nameof(articleRepositories));
            _tagRepositories = tagRepositories ?? throw new ArgumentNullException(nameof(tagRepositories));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<CreateArticleCommandResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var result = new CreateArticleCommandResult();
            try
            {

                var tags = await _tagRepositories.GetTagsByNamesAsync(request.Tags.ToList(), cancellationToken);
                var article = Article.Create(tags, request.Tags, request.Name, _timeProvider);
                article.AddDomainEvents(new CreatedArticleEvent(article.Id));
                await _articleRepositories.AddArticleAsync(article, cancellationToken);
                await _unitOfWork.SaveChangesAsync();

                // сохранение юнитом и запуск интерцепторов

                result.ID = article.Id.Value;

            }
            catch (Exception ex)
            {
                result.MessageError = ex.Message;
            }

            return result;
        }
    }
}

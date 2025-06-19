using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Events;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Repositories;

namespace WebForWork.Application.Commands
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreateArticleCommandResult>
    {
        private readonly TimeProvider _timeProvider;
        private readonly IArticleRepositories _articleRepositories;
        private readonly IUnitOfWork _unitOfWork;
        public CreateArticleCommandHandler(TimeProvider timeProvider, IArticleRepositories articleRepositories, IUnitOfWork unitOfWork) 
        {
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
            _articleRepositories = articleRepositories ?? throw new ArgumentNullException(nameof(articleRepositories));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<CreateArticleCommandResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var result = new CreateArticleCommandResult();
            try
            {
                var article = Article.Create(request.Tags, request.Name, _timeProvider);
                article.AddDomainEvents(new CreatedArticleEvent(article.Id));
                await _articleRepositories.AddArticleAsync(article, cancellationToken);
                await _unitOfWork.SaveChangesAsync();

                // сохранение юнитом и запуск интерцепторов

                result.ID = article.Id.Value;

            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}

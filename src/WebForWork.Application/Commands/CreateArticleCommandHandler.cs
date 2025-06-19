using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Entities;

namespace WebForWork.Application.Commands
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreateArticleCommandResult>
    {
        private readonly TimeProvider _timeProvider;
        public CreateArticleCommandHandler(TimeProvider timeProvider) 
        {
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        }
        public Task<CreateArticleCommandResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var article = Article.Create(request.Tags,request.Name,_timeProvider);
                // вызов репозитория
                // сохранение юнитом
            }
            catch (Exception ex)
            {

            }

            throw new NotImplementedException();
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Events;

namespace WebForWork.Application.Commands
{
    public class CreateArticleNotificationHandler : INotificationHandler<CreatedArticleEvent>
    {
        public Task Handle(CreatedArticleEvent notification, CancellationToken cancellationToken)
        {

            // TODO работа с агрегатом разделов, автоматическое создание
            throw new NotImplementedException();
        }
    }
}

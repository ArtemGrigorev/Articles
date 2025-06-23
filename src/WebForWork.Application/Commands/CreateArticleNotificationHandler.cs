using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Events;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Repositories;

namespace WebForWork.Application.Commands
{
    public class CreateArticleNotificationHandler : INotificationHandler<CreatedArticleEvent>
    {
        private readonly IScopeRepositories _scopeRepositories;
        public CreateArticleNotificationHandler(
            IServiceScopeFactory serviceScopeFactory,
            IScopeRepositories scopeRepositories)
        {
            _scopeRepositories = scopeRepositories ?? throw new ArgumentNullException(nameof(scopeRepositories));
        }

        public async Task Handle(CreatedArticleEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                Task task = new Task(async () =>
                {
                    var article = await _scopeRepositories.GetArticleAsNoTrackingScopeAsync(notification.ArticleId, cancellationToken);
                    var tags = article.Tags.OrderBy(x => x.order).Select(x => x.tag);
                    var exist = _scopeRepositories.ExistChapterByTagsScope(tags);

                    if (!exist)
                    {
                        var chapter = Chapter.Create(tags);
                        await _scopeRepositories.AddAndSaveChapterScopeAsync(chapter, cancellationToken);
                    }
                }
                );
                task.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

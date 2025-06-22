using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Events;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Repositories;

namespace WebForWork.Application.Commands
{
    public class CreateArticleNotificationHandler : INotificationHandler<CreatedArticleEvent>
    {
        private readonly IArticleRepositories _articleRepositories;
        private readonly IChapterRepositories _chapterRepositories;
        private readonly IUnitOfWork _unitOfWork;
        public CreateArticleNotificationHandler(IArticleRepositories articleRepositories, 
            IChapterRepositories chapterRepositories,
            IUnitOfWork unitOfWork)
        {
            _articleRepositories = articleRepositories ?? throw new ArgumentNullException(nameof(articleRepositories));
            _chapterRepositories = chapterRepositories ?? throw new ArgumentNullException(nameof(chapterRepositories)); 
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(CreatedArticleEvent notification, CancellationToken cancellationToken)
        {

            try
            {

                var article = await _articleRepositories.GetArticleAsNoTrackingAsync(notification.ArticleId, cancellationToken);
                var tags = article.Tags.OrderBy(x=>x.order).Select(x => x.tag);
                var exist = _chapterRepositories.ExistChapterByTags(tags);

                if (!exist)
                {
                    var chapter = Chapter.Create(tags);
                    await  _chapterRepositories.AddChapterAsync(chapter, cancellationToken);
                    await _unitOfWork.SaveChangesAsync();
                }




/*
                Task task = new Task(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine("Hello Task!");
                    }
                });

                task.Start();*/

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

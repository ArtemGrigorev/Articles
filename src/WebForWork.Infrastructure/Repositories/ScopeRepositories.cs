using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Models.ValueObject;
using WebForWork.Domain.Repositories;
using WebForWork.Infrastructure.Exceptions;

namespace WebForWork.Infrastructure.Repositories
{
    public class ScopeRepositories : IScopeRepositories
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ScopeRepositories(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<Article> GetArticleAsNoTrackingScopeAsync(ArticleId articleId, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<WebForWorkDbContext>();

                    return await context.Articles
                        .AsNoTracking()
                        .Where(x => x.Id == articleId)
                        .Include(x => x.Tags)
                          .ThenInclude(x => x.tag)
                        .SingleAsync(cancellationToken);
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new ArticleRepositoriesException($"Cтатья не найдена Id = {articleId.Value}", ex);
            }
            catch (Exception ex)
            {
                throw new ArticleRepositoriesException($"Ошибка получения статьи Id = {articleId.Value}", ex);
            }

        }

        public bool ExistChapterByTagsScope(IEnumerable<Tag> tags)
        {
            bool result = false;

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<WebForWorkDbContext>();

                    var chaptersTags = context.Chapters
                    .AsNoTracking()
                    .Include(chapter => chapter.Tags)
                    .SelectMany(x => x.Tags);

                    var exceptTags = chaptersTags.Where(x => tags.Select(x => x.Id).Contains(x.tag.Id));

                    result = exceptTags
                          .GroupBy(p => p.chapterId)
                          .Select(g => new { ChapteId = g.Key, Count = g.Count() })
                          .Any(x => x.Count >= tags.Count());
                }
            }
            catch (Exception ex)
            {
                throw new ChapterRepositoriesException("Ошибка поиска раздела по тегам", ex);
            }

            return result;
        }

        public async Task AddAndSaveChapterScopeAsync(Chapter chapter, CancellationToken cancellationToken)
        {

            if (cancellationToken.IsCancellationRequested)
                return;

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<WebForWorkDbContext>();
                    await context.Chapters.AddAsync(chapter,cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                }

            }
            catch (Exception ex)
            {
                throw new ChapterRepositoriesException("Ошибка создания раздела", ex);
            }
        }
    }
}

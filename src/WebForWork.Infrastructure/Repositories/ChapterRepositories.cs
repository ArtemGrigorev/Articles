using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Models.ValueObject;
using WebForWork.Domain.Repositories;
using WebForWork.Infrastructure.Exceptions;

namespace WebForWork.Infrastructure.Repositories
{
    public class ChapterRepositories : IChapterRepositories
    {
        private readonly WebForWorkDbContext _context;

        public ChapterRepositories(WebForWorkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddChapterAsync(Chapter chapter, CancellationToken cancellationToken)
        {

            if (cancellationToken.IsCancellationRequested)
                return;

            try
            {
                await _context.Chapters.AddAsync(chapter);
            }
            catch (Exception ex)
            {
                throw new ChapterRepositoriesException("Ошибка создания раздела", ex);
            }
        }

        public bool ExistChapterByTagsAsync(IEnumerable<Tag> tags, CancellationToken cancellationToken)
        {
            bool result = false;

            if (cancellationToken.IsCancellationRequested)
                return result;

            try
            {
                var chapters = _context.Chapters
                        .AsNoTracking()
                        .Include(chapter => chapter.Tags);

                var chaptersTags = chapters.SelectMany(x => x.Tags).Where(x => tags.Contains(x.tag));

                result = chaptersTags.GroupBy(p => p.tagId)
                    .Select(g => new { Count = g.Count() }).Any(x => x.Count == tags.Count());
            }
            catch (Exception ex)
            {
                throw new ChapterRepositoriesException("Ошибка поиска раздела по тегам", ex);
            }

            return result;
        }

        public async Task<Chapter> GetChapterAsNoTrackingAsync(ChapterId chapterId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return null;

            try
            {
                return await _context.Chapters
                    .AsNoTracking()
                    .Where(x => x.Id == chapterId)
                    .Include(x => x.Tags)
                      .ThenInclude(x => x.tag)
                    .SingleAsync(cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                throw new ChapterRepositoriesException($"Cтатья не найдена Id = {chapterId.Value}", ex);
            }
            catch (Exception ex)
            {
                throw new ChapterRepositoriesException($"Ошибка получения статьи Id = {chapterId.Value}", ex);
            }
        }
    }
}

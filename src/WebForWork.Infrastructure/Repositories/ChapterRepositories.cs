using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Events;
using WebForWork.Domain.Models;
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

        public bool ExistChapterByTags(IEnumerable<Tag> tags)
        {
            bool result = false;

            try
            {
                var chaptersTags = _context.Chapters
                    .AsNoTracking()
                    .Include(chapter => chapter.Tags)
                    .SelectMany(x => x.Tags);

                var exceptTags = chaptersTags.Where(x => tags.Select(x => x.Id).Contains(x.tag.Id));

                result = exceptTags
                      .GroupBy(p => p.chapterId)
                      .Select(g => new { ChapteId = g.Key, Count = g.Count() })
                      .Any(x => x.Count >= tags.Count());
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

        public List<ChaptersCatalogDTO> GetChaptersAsNoTracking(byte page)
        {
            const byte size = 10;
            var result = new List<ChaptersCatalogDTO> { };
            int limit = (page - 1) * size;
            try
            {
                result = _context.Database.SqlQuery<ChaptersCatalogDTO>
                   ($@"SELECT t2.""name"" , t2.id , count(t2.id) as count
                         FROM webforwork.chapters t2   inner join             
                          (SELECT  distinct(art.""id"") idart, c0.id as id FROM webforwork.chapters AS c0
                          INNER JOIN webforwork.chapters_tags AS ct ON ct.""chapterId"" = c0.""id""
                          INNER JOIN webforwork.articles_tags AS artt ON artt.""tagId"" = ct.""tagId""
                          INNER JOIN webforwork.articles AS art ON art.""id"" = artt.""articleId"") t1 ON t1.id = t2.id
                         GROUP BY (t2.id)
                         ORDER BY count DESC LIMIT {size} OFFSET {limit}")
                   .AsEnumerable()
                   .ToList();
            }
            catch (Exception ex)
            {
                throw new ChapterRepositoriesException($"Ошибка получения каталога разделов", ex);
            }

            return result;
        }
    }
}

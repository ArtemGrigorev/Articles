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
                throw new ChapterRepositoriesException($"Ошибка создания раздела {chapter.Id.Value}", ex);
            }
        }



        /*
        public > ChaptersByTagsAsync(IEnumerable<Tag> tags, CancellationToken cancellationToken)
        {
            var result = new List<Chapter> { };
            if (cancellationToken.IsCancellationRequested)
                return result;

            try
            {

                var chapters = _context.Chapters
                        .AsNoTracking()
                        .Include(chapter => chapter.Tags);        

                var chaptersTags = chapters.SelectMany(x => x.Tags).Where(x => tags.Contains(x.tag));

                var existsUniqeChapter = chaptersTags.GroupBy(p => p.tagId)
                    .Select(g => new { Name = g.Key, Count = g.Count() }).Any(x=>x.Count == tags.Count());


                  //  .Select(g => new { Name = g.Key, Count = g.Count() });


                //Where(x => x.Tags.SelectMany(z=>z.tag))

                // .SelectMany(ch => ch.Tags)

                // .ThenInclude(ch => ch.)
                //   .SelectMany(ch => ch.Tags)
                //  .Where(ch => tags.Contains())


              /*  result = await _context.Chapters
                    .AsNoTracking()
                    .Where(t => tagNames.Contains(t.Name))
                    .ToListAsync(cancellationToken);*/

        /*
                    }
                    catch (Exception ex)
                    {
                        throw new ChapterRepositoriesException("Ошибка получения разделов по связанным тегам", ex);
                    }

                    return result;
                }
        */
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
    }
}

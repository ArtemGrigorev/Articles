using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Repositories
{
    public interface IChapterRepositories
    {
        Task AddChapterAsync(Chapter chapter, CancellationToken cancellationToken);
        Task<Chapter> GetChapterAsNoTrackingAsync(ChapterId chapterId, CancellationToken cancellationToken);
        List<ChaptersCatalogDTO> GetChaptersAsNoTracking(byte page);
        bool ExistChapterByTags(IEnumerable<Tag> tags);
    }
}

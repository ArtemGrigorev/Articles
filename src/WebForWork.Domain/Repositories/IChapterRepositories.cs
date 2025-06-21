using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Repositories
{
    public interface IChapterRepositories
    {
        Task AddChapterAsync(Chapter chapter, CancellationToken cancellationToken);
        bool ExistChapterByTagsAsync(IEnumerable<Tag> tags, CancellationToken cancellationToken);
    }
}

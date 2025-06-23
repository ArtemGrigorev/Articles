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
    public interface IScopeRepositories
    {
        Task<Article> GetArticleAsNoTrackingScopeAsync(ArticleId articleId, CancellationToken cancellationToken);
        bool ExistChapterByTagsScope(IEnumerable<Tag> tags);
        Task AddAndSaveChapterScopeAsync(Chapter chapter, CancellationToken cancellationToken);
    }
}

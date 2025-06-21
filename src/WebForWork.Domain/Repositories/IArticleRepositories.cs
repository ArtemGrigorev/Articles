using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Repositories
{
    public interface IArticleRepositories 
    {
        Task AddArticleAsync(Article article, CancellationToken cancellationToken);
        Task<Article> GetArticleAsNoTrackingAsync(ArticleId articleId, CancellationToken cancellationToken);
        Task<Article> GetArticleAsync(ArticleId articleId, CancellationToken cancellationToken);
    }
}

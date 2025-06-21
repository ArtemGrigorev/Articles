using Microsoft.EntityFrameworkCore;
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
    public class ArticleRepositories : IArticleRepositories
    {
        private readonly WebForWorkDbContext _context;

        public ArticleRepositories(WebForWorkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddArticleAsync(Article article, CancellationToken cancellationToken)
        {

            if (cancellationToken.IsCancellationRequested)
                return;

            try
            {
                await _context.Articles.AddAsync(article);
            }
            catch (Exception ex) 
            {
                throw new ArticleRepositoriesException($"Ошибка создания статьи Id = {article.Id.Value}", ex);
            }
        }

        public async Task<Article> GetArticleAsync(ArticleId articleId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return null;

            try
            {
                return await _context.Articles
                    .Where(x => x.Id == articleId)
                    .Include(x => x.Tags)
                      .ThenInclude(x => x.tag)
                    .SingleAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ArticleRepositoriesException($"Ошибка получения статьи Id = {articleId.Value}", ex);
            }
        }

        public async Task<Article> GetArticleAsNoTrackingAsync(ArticleId articleId, CancellationToken cancellationToken)
        {

            if (cancellationToken.IsCancellationRequested)
                return null;

            try
            {
                return await _context.Articles
                    .AsNoTracking()
                    .Where(x => x.Id == articleId)
                    .Include(x => x.Tags)
                      .ThenInclude(x => x.tag)
                    .SingleAsync(cancellationToken);
            }
            catch(InvalidOperationException ex)
            {
                throw new ArticleRepositoriesException($"Cтатья не найдена Id = {articleId.Value}", ex);
            }
            catch (Exception ex)
            {
                throw new ArticleRepositoriesException($"Ошибка получения статьи Id = {articleId.Value}", ex);
            }
        }

        public async Task<List<Article>> GetArticlesByTagsAsync(IEnumerable<Tag> tags, CancellationToken cancellationToken)
        {
            var result = new List<Article> { };

            if (cancellationToken.IsCancellationRequested)
                return result;

            try
            {
                var articles = _context.Articles
                        .AsNoTracking()
                        .Include(articles => articles.Tags);

                var articlesTags = articles.SelectMany(x => x.Tags)
                        .Include(x=>x.article)
                        .Where(x => tags.Contains(x.tag));

                result = await articlesTags.Select(at => at.article).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ArticleRepositoriesException("Ошибка получения стетей в разделе", ex);
            }

            return result;
        }
    }
}

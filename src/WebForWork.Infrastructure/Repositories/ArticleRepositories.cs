using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Repositories;

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
            
            }
        }
    }
}

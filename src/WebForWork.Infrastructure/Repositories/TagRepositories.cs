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

namespace WebForWork.Infrastructure.Repositories
{
    public class TagRepositories : ITagRepositories
    {
        private readonly WebForWorkDbContext _context;

        public TagRepositories(WebForWorkDbContext context)
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

        public async Task<List<Tag>> GetTagsByNamesAsync(IList<string> names, CancellationToken cancellationToken)
        {
            var result = new List<Tag> { };
            if (cancellationToken.IsCancellationRequested)
                return result;
            try
            {
                result = await _context.Tags.Where(t => names.Contains(t.Name.Value)).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                //TODO логировать или создать пользовательское 
            }
            return result;
        }
    }
}

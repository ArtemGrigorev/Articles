using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                throw new TagRepositoriesException("Ошибка добавления статьи в базу данных", ex);
            }
        }
      
        public async Task<List<Tag>> GetTagsByNamesAsync(IEnumerable<TagName> tagNames, CancellationToken cancellationToken)
        {
            var result = new List<Tag> { };
            if (cancellationToken.IsCancellationRequested)
                return result;

            try
            {
                  result = await _context.Tags
                      .AsNoTracking()
                      .Where(t => tagNames.Contains(t.Name))
                      .ToListAsync(cancellationToken);


            }
            catch (Exception ex)
            {
                 throw new TagRepositoriesException("Ошибка получения тегов по названиям", ex);
            }

            return result;
        }
    }
}

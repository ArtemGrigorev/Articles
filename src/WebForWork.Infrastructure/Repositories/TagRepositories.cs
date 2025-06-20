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
        private string s(Tag t)
        {
            return t.Name.Value;
        }
      
        public async Task<List<Tag>> GetTagsByNamesAsync(List<string> names, CancellationToken cancellationToken)
        {
            var result = new List<Tag> { };
            if (cancellationToken.IsCancellationRequested)
                return result;
            try
            {
                // string values = 
                /*  result = await _context.Tags
                      .AsNoTracking()
                      .Where(t => names.Contains(s(t)))
                      .ToListAsync(cancellationToken);*/
                //  NpgsqlParameter param = new NpgsqlParameter("@name", "%test1%");

                var columnValue =  "test1";
                var sql = "SELECT id, name FROM webforwork.tags where name = @param";


                /* NpgsqlParameter param = new NpgsqlParameter();
                 param.ParameterName = "@Mark";
                 param.Value = "test1";
                 param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;*/
                /*    result = await _context.Tags
                        .FromSql($"SELECT id, name FROM webforwork.tags where @name = {columnValue}")
                        .AsSplitQuery()
                        .ToListAsync();*/
                /* var customParam = new  { Prop1 = "value1", Prop2 = 42 };

                 var searchParam = new NpgsqlParameter("searchParam", $"%test1%");
                 var results = await _context.Tags
                     .FromSqlInterpolated(sql).ToList();*/

                //.FromSqlRaw("SELECT id, name FROM webforwork.tags where name = @searchParam;", searchParam).AsSplitQuery().ToListAsync();

                string value = "3";
                var searchParam = new NpgsqlParameter("searchParam", value);
                var command = await _context.Tags.FromSqlRaw($"SELECT id, name FROM webforwork.tags where name = @searchParam;", searchParam).ToListAsync();

            }
            catch (Exception ex)
            {
                 throw new TagRepositoriesException("Ошибка получения тегов по названиям", ex);
            }
            return result;
        }
    }
}

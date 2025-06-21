using Microsoft.EntityFrameworkCore;
using WebForWork.Domain.Models.Aggregates;
using WebForWork.Domain.Models.Entities;
using WebForWork.Domain.Repositories;

namespace WebForWork.Infrastructure
{
    public class WebForWorkDbContext : DbContext, IUnitOfWork
    {
        public const string SchemeName = "webforwork";

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public WebForWorkDbContext(DbContextOptions<WebForWorkDbContext> options) : base(options) 
        {

        }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(SchemeName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebForWorkDbContext).Assembly);
        }
    }
}

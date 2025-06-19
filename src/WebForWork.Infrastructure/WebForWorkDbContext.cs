using Microsoft.EntityFrameworkCore;
using WebForWork.Domain.Models.Entities;

namespace WebForWork.Infrastructure
{
    public class WebForWorkDbContext : DbContext
    {
        public const string SchemeName = "webforwork";

        public DbSet<Article> Articles { get; set; }
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

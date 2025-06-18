using Microsoft.EntityFrameworkCore;

namespace WebForWork.Infrastructure
{
    public class WebForWorkDbContext : DbContext
    {
        public const string SchemeName = "webforwork";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(SchemeName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebForWorkDbContext).Assembly);
        }
    }
}

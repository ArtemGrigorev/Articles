using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDatabaseStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebForWorkDbContext>((sp, opt) => 
            {
                opt.UseNpgsql(configuration.GetConnectionString("DatabaseSettings"),
                contexOptions =>
                {
                    contexOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, WebForWorkDbContext.SchemeName);
                });
            });
        }

        public static async Task ApplyMigration(this IHost host)
        {
            var configuration = host.Services.GetRequiredService<IConfiguration>();

            if (!configuration.GetValue<bool>("Database:Migrations"))
                return;

            var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            await using var scope = scopeFactory.CreateAsyncScope();
            await using var context = scope.ServiceProvider.GetRequiredService<WebForWorkDbContext>();
            await context.Database.MigrateAsync();
        }

    }
}

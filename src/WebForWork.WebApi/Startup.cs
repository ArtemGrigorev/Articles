using WebForWork.Infrastructure.Extensions;

namespace WebForWork.WebApi
{
    internal static class Startup
    {
        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.RegisterDatabaseStore(builder.Configuration);
        }
    }
}

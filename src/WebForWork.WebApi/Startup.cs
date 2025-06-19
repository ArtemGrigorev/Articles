using WebForWork.Application.Extensions;
using WebForWork.Domain.Repositories;
using WebForWork.Infrastructure.Extensions;
using WebForWork.Infrastructure.Repositories;

namespace WebForWork.WebApi
{
    internal static class Startup
    {
        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.RegisterDatabaseStore(builder.Configuration);
            builder.Services.AddSingleton(TimeProvider.System);
            builder.Services.RegisterMediatR();
        }
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WebForWork.Application.Extensions;
using WebForWork.Domain.Repositories;
using WebForWork.Infrastructure.Extensions;
using WebForWork.Infrastructure.Repositories;
using WebForWork.WebApi.Configurations;

namespace WebForWork.WebApi
{
    internal static class Startup
    {
        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddValidatorsFromAssemblyContaining<CreateArticleValidator>();
            builder.Services.RegisterDatabaseStore(builder.Configuration);
            builder.Services.AddSingleton(TimeProvider.System);
            builder.Services.RegisterMediatR();
        }
    }
}

using AutoMapper;
using WebForWork.Application.Commands;
using WebForWork.WebApi.Models;

namespace WebForWork.WebApi.Configurations
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration() 
        {
            CreateMap<CreateRequestModel, CreateArticleCommand>();
            CreateMap<CreateArticleCommandResult, CreateResponseModel>();
        }
    }
}

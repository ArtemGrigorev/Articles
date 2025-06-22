using AutoMapper;
using WebForWork.Application;
using WebForWork.Application.Commands;
using WebForWork.WebApi.Models.Article;

namespace WebForWork.WebApi.Configurations.Mappers
{
    public class ArticleMapperConfiguration : Profile
    {
        public ArticleMapperConfiguration() 
        {
            CreateMap<CreateRequestModel, CreateArticleCommand>();
            CreateMap<CreateArticleCommandResult, CreateResponseModel>();

            CreateMap<UpdateRequestModel, UpdateArticleCommand>();

            CreateMap<GetRequestModel, GetArticleCommand>();
            CreateMap<GetArticleCommandResult, GetResponseModel>();
          
        }
    }
}

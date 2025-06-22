using AutoMapper;
using WebForWork.Application;
using WebForWork.Application.Commands.CreateArticle;
using WebForWork.Application.Commands.GetArticle;
using WebForWork.Application.Commands.UpdateArticle;
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

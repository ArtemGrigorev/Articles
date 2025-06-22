using AutoMapper;
using WebForWork.Application;
using WebForWork.Application.Commands.GetChaptersCatalog;
using WebForWork.Application.Commands.GetChapterWithArticles;
using WebForWork.WebApi.Models.Chapter;

namespace WebForWork.WebApi.Configurations.Mappers
{
    public class ChapterMapperConfiguration : Profile
    {
        public ChapterMapperConfiguration()
        {
            CreateMap<AttributesApplicationDTO, AttributesPresentsDto>();

            CreateMap<GetRequestModel, GetArticlesInChapterCommand>();
            CreateMap<GetArticlesInChapterResult, GetResponseModel>();

            CreateMap<GetCatalogRequestModel, GetChaptersCatalogCommand>();
            CreateMap<GetChaptersCatalogResult, GetCatalogResponseModel>();
        }
    }
}

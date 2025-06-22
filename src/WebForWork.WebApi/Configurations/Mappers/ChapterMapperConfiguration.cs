using AutoMapper;
using WebForWork.Application;
using WebForWork.Application.Commands;
using WebForWork.WebApi.Models.Chapter;

namespace WebForWork.WebApi.Configurations.Mappers
{
    public class ChapterMapperConfiguration : Profile
    {
        public ChapterMapperConfiguration()
        {
            CreateMap<NameAndIdArticleDtoApplicationDTO, NameAndIdArticlePresentDto>();

            CreateMap<GetRequestModel, GetArticlesInChapterCommand>();
            CreateMap<GetArticlesInChapterResult, GetResponseModel>();
        }
    }
}

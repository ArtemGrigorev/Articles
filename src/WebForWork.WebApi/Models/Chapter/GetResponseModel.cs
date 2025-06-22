
namespace WebForWork.WebApi.Models.Chapter
{
    public class GetResponseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<AttributesPresentsDto> ArticlesAttributes { get; set; }

        public string MessageError { get; set; } = string.Empty;
    }
}

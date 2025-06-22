namespace WebForWork.WebApi.Models.Chapter
{
    public class GetCatalogResponseModel
    {
        public IEnumerable<AttributesPresentsDto> Attributes { get; set; }

        public string MessageError { get; set; } = string.Empty;
    }
}

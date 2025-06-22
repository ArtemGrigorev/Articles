namespace WebForWork.WebApi.Models.Article
{
    public class CreateResponseModel
    {
        public Guid Id { get; set; }
        public string MessageError { get; set; } = string.Empty;
    }
}

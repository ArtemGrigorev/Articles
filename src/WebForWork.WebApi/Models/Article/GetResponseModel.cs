using System.ComponentModel.DataAnnotations;

namespace WebForWork.WebApi.Models.Article
{
    public class GetResponseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<string> Tags { get; set; }

        public string MessageError { get; set; } = string.Empty;
    }
}

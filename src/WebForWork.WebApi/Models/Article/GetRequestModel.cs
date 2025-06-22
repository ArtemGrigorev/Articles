using System.ComponentModel.DataAnnotations;

namespace WebForWork.WebApi.Models.Article
{
    public class GetRequestModel
    {
        [Required]
        public string Id { get; set; }
    }
}

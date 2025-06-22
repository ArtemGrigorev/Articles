using System.ComponentModel.DataAnnotations;

namespace WebForWork.WebApi.Models.Article
{
    public class CreateRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<string> Tags { get; set; }

    }
}

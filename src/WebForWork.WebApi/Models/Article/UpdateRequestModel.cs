using System.ComponentModel.DataAnnotations;

namespace WebForWork.WebApi.Models.Article
{
    public class UpdateRequestModel
    {

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<string> Tags { get; set; }

    }
}

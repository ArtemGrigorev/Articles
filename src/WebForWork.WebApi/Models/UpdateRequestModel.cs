using System.ComponentModel.DataAnnotations;

namespace WebForWork.WebApi.Models
{
    public class UpdateRequestModel
    {

        [Required]
        public string Guid { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<string> Tags { get; set; }

    }
}

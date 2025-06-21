using System.ComponentModel.DataAnnotations;

namespace WebForWork.WebApi.Models
{
    public class GetRequestModel
    {
        [Required]
        public string Id { get; set; }
    }
}

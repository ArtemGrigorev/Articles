using System.ComponentModel.DataAnnotations;

namespace WebForWork.WebApi.Models.Chapter
{
    public class GetRequestModel
    {
        [Required]
        public string Id { get; set; }
    }
}

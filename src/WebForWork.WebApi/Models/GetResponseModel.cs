using System.ComponentModel.DataAnnotations;

namespace WebForWork.WebApi.Models
{
    public class GetResponseModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}

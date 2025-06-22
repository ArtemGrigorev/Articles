using System.ComponentModel.DataAnnotations;

namespace WebForWork.WebApi.Models.Chapter
{
    public class GetCatalogRequestModel
    {
        string page;
        public string Page
        {
            set
            {
                page = int.Parse(value) == 0 ? "1" : value;
            }
            get
            {
                return page;
            }
        }
    }
}

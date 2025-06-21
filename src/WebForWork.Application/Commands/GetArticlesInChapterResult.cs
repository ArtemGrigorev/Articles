using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands
{
    public class GetArticlesInChapterResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> ArticlesName { get; set; } = new List<string>();
        public string MessageError { get; set; } = string.Empty;
    }
}

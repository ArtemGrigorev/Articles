using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands
{
    public class GetArticlesInChapterResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

     //   [DataMember]
        public IEnumerable<NameAndIdArticleDtoApplicationDTO> ArticlesAttributes { get; set; }
        public string MessageError { get; set; } = string.Empty;
    }
}

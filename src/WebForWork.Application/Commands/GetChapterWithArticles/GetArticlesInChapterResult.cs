using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands.GetChapterWithArticles
{
    public class GetArticlesInChapterResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<AttributesApplicationDTO> Attributes { get; set; }
        public string MessageError { get; set; } = string.Empty;
    }
}

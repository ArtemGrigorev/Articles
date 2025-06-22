using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Commands.GetChaptersCatalog
{
    public class GetChaptersCatalogResult
    {
        public IEnumerable<AttributesApplicationDTO> Attributes { get; set; }
        public string MessageError { get; set; } = string.Empty;

    }
}

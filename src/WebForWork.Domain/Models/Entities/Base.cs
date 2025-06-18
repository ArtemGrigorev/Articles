using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public class Base
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }
    }
}

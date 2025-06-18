using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; protected init; }
        public string Name { get; private set; }

   /*     private readonly List<TagId> _tag = new();
        public IReadOnlyList<TagId> Tags => _tag.AsReadOnly();*/

    }
}

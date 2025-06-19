using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Models.ValueObject;

namespace WebForWork.Domain.Models.Entities
{
    public abstract class BaseEntity<T,K>
    {
        public T Id { get; protected init; }
        public K Name { get; protected init; }
        protected BaseEntity(T id, K name)
        {
            Id = id;
            Name = name;
        }
    }
}

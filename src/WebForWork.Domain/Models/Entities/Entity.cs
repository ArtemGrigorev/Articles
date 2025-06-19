using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Events;

namespace WebForWork.Domain.Models.Entities
{
    public abstract class Entity<T, K> : BaseEntity<T, K>, IHasDomainEvents
    {

        private readonly List<IDomainEvents> _domainEvents = new();
        public ICollection<IDomainEvents> DomainEvents => _domainEvents.AsReadOnly();
        protected Entity(T id, K name) : base(id, name)
        {
        }

        public void AddDomainEvents(IDomainEvents @events) 
        {
            _domainEvents.Add(@events);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

    }
}

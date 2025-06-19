using MediatR;
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

        private readonly List<INotification> _domainEvents = new();
        public ICollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
        protected Entity(T id, K name) : base(id, name)
        {
        }

        public void AddDomainEvents(INotification @events) 
        {
            _domainEvents.Add(@events);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

    }
}

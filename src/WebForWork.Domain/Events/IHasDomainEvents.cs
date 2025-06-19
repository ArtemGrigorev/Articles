using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Domain.Events
{
    public interface IHasDomainEvents
    {
       public ICollection<INotification> DomainEvents { get; }
       public void ClearDomainEvents();
       public void AddDomainEvents(INotification domainEvents);
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Application.Events
{
    public interface IDomainEvent : INotification
    {
    }
}

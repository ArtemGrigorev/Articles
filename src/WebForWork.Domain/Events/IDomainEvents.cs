using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Domain.Events
{
    public interface IDomainEvents : IRequest<Result>
    {

    }
}

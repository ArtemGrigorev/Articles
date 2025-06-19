using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Domain.Events
{
    public sealed record CreatedArticleEvent(Guid Value): IDomainEvents;

}

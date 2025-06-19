using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForWork.Infrastructure.Intrceptors
{
    public class PublishDomainEventsInterceptors : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
            InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken).ConfigureAwait(false);
        }

        private async Task PublishDomainEventsAsync(DbContext dbContext)
        { 
        
        }
    }
}

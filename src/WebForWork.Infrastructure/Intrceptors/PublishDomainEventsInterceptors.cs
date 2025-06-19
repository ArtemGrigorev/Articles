using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForWork.Domain.Events;

namespace WebForWork.Infrastructure.Intrceptors
{
    public class PublishDomainEventsInterceptors : SaveChangesInterceptor
    {
        private readonly IServiceProvider _serviceProvider;

        public PublishDomainEventsInterceptors(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
            InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken).ConfigureAwait(false);
        }

        private async Task PublishDomainEventsAsync(DbContext dbContext)
        { 
             if (dbContext is null)
                return;

             var entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
                .Where(entry => entry.Entity.DomainEvents.Any())
                .Select(entry => entry.Entity)
                .ToList();

             var domainEvents = entitiesWithDomainEvents
                .SelectMany(entry => entry.DomainEvents)
                .ToList();

            if (domainEvents.Count == 0)
                return;

            entitiesWithDomainEvents.ForEach(entry => entry.ClearDomainEvents());
            var publishEndpoint = _serviceProvider.GetRequiredService<IPublisher>();


        }
    }
}

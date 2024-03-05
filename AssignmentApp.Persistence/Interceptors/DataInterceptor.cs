using AssignmentApp.Application.Interfaces;
using AssignmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AssignmentApp.Persistence.Interceptors
{
    public class DataInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if (dbContext is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var entries = dbContext.ChangeTracker.Entries<Assignment>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Property(x => x.DateCreated).CurrentValue = DateTime.UtcNow;
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}

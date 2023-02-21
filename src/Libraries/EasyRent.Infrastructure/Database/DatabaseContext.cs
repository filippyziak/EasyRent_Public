using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Database;
using Microsoft.EntityFrameworkCore;

namespace EasyRent.Infrastructure.Database;

public abstract class DatabaseContext<TDbContext> : DbContext
    where TDbContext : DbContext
{
    protected DatabaseContext(DbContextOptions<TDbContext> options) : base(options)
    {
    }

    protected abstract string Schema { get; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BasePersistenceModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
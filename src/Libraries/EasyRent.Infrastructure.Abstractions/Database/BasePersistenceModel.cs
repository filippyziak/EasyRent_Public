using System;

namespace EasyRent.Infrastructure.Abstractions.Database;

public abstract class BasePersistenceModel
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedOn { get; set; }
}
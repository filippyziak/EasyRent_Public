using System;

namespace EasyRent.Identity.Integrations.Identity;

public interface IIdentityIntegrationEvent
{
    public IdentityEventType EventType { get; }
    public Guid AccountId { get; }
}
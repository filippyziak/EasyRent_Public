namespace EasyRent.MessageBroker;

public record MessageBrokerConfiguration
{
    public string HostName { get; init; } = "localhost";
    public int Port { get; init; }
    public long DlqMessageTtlInMilliseconds { get; init; } = 1000;
}
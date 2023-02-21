namespace EasyRent.MessageBroker;

public interface IMessage
{
    string MessageId { get; }
    string Type { get; }
    long Timestamp { get; }
    object Data { get; }
}
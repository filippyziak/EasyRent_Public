using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace EasyRent.EventSourcing.EventStore;

public static class EventSerializer
{
    public static byte[] Serialize(object data)
        => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

    public static IDomainEvent Deserialize(this ResolvedEvent resolvedEvent)
    {
        var meta = JsonConvert.DeserializeObject<EventMetadata>(
            Encoding.UTF8.GetString(resolvedEvent.Event.Metadata));
        var dataType = Type.GetType(meta.ClrType);
        var jsonData = Encoding.UTF8.GetString(resolvedEvent.Event.Data);

        var data = JsonConvert.DeserializeObject(jsonData, dataType);
        return (IDomainEvent)data;
    }
}
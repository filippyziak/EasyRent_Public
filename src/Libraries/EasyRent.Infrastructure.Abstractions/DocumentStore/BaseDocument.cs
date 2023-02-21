using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyRent.Infrastructure.Abstractions.DocumentStore;

public abstract class BaseDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;
    public DateTime? ModifiedOn { get; set; }
}
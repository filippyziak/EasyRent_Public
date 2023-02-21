using System;

namespace EasyRent.Infrastructure.DocumentStore;

public class BsonCollectionAttribute : Attribute
{
    public string CollectionName { get; }

    public BsonCollectionAttribute(string collectionName) => CollectionName = collectionName;
}
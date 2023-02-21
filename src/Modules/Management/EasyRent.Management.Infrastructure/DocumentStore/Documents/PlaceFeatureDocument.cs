using System;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Infrastructure.DocumentStore;

namespace EasyRent.Management.Infrastructure.DocumentStore.Documents;

[BsonCollection("PlaceFeatures")]
public class PlaceFeatureDocument : BaseDocument
{
    public Guid PlaceFeatureId { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
}
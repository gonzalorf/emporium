using Emporium.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace Emporium.Infrastructure.Context;

public class DataObject<T> : IDataObject<T> where T : IEntity
{
    public DataObject(string id, string partitionKey, string objectType, T entity,
        string eTag,
        int ttl,
        EntityState state = EntityState.Unmodified
    )
    {
        Id = id;
        PartitionKey = partitionKey;
        Type = objectType;
        Data = entity;
        Ttl = ttl;
        Etag = eTag;
        State = state;
    }

    [JsonPropertyName("id")] 
    public string Id { get; private set; }
    public string PartitionKey { get; private set; }
    public string Type { get; private set; }
    public T Data { get; set; }
    [JsonPropertyName("_etag")] 
    public string Etag { get; set; }
    public int Ttl { get; }

    [JsonIgnore] 
    public EntityState State { get; set; }
}
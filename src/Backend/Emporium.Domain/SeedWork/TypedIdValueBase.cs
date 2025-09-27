using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Emporium.Domain.SeedWork;

public abstract record TypedIdValueBase(Guid Value);

public class TypedIdValueConverter<T> : JsonConverter<T> where T : TypedIdValueBase
{
    public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
    {
        if (value is null)
            throw new JsonSerializationException($"{typeof(T).Name} is required");

        writer.WriteValue(value.Value);
    }

    public override T ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var guid = Guid.Parse(reader.Value?.ToString()!);

        var ctor = typeof(T).GetConstructor(new[] { typeof(Guid) });
        if (ctor is null)
            throw new JsonSerializationException($"No suitable constructor found for {typeof(T).Name}");

        return (T)ctor.Invoke(new object[] { guid });
    }
}


//[JsonConverter(typeof(TypedIdValueBaseConverter))]
//public record TypedIdValueBase : IEquatable<TypedIdValueBase>
//{
//    public Guid Value { get; }

//    public bool IsEmpty()
//    {
//        return Value == Guid.Empty;
//    }

//    public TypedIdValueBase(Guid value)
//    {
//        Value = value;
//    }

//    public override int GetHashCode()
//    {
//        return Value.GetHashCode();
//    }

//    public override string ToString()
//    {
//        return Value.ToString();
//    }
//}

//class TypedIdValueBaseConverter : JsonConverter
//{
//    public override bool CanConvert(Type objectType)
//    {
//        return (objectType == typeof(TypedIdValueBase));
//    }

//    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//    {
//        return new TypedIdValueBase(new Guid(JToken.Load(reader).ToString())); 
//    }

//    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//    {
//        JToken.FromObject(((TypedIdValueBase)value).Value.ToString()).WriteTo(writer);
//    }
//}
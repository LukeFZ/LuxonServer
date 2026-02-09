using System.Diagnostics;

namespace LuxonServer.Serialization;

public static class ValueSerialization
{
    public static byte[] Serialize(object? obj)
    {
        var serializer = new ValueSerializer();
        serializer.WriteObject(obj);
        return serializer.Buffer;
    }

    public static object? Deserialize(ReadOnlySpan<byte> data)
        => new ValueDeserializer(data).ReadObject();

    public static T DeserializeAs<T>(ReadOnlySpan<byte> data)
    {
        var obj = Deserialize(data);
        Debug.Assert(obj != null);
        return (T)obj;
    }
}
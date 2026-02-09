using System.Diagnostics;

namespace LuxonServer.Serialization;

public static class VariantSerialization
{
    public static byte[] Serialize(object? obj)
    {
        var serializer = new Serializer();
        serializer.WriteObject(obj);
        return serializer.Buffer;
    }

    public static object? Deserialize(ReadOnlySpan<byte> data)
        => new Deserializer(data).ReadObject();

    public static T DeserializeAs<T>(ReadOnlySpan<byte> data)
    {
        var obj = Deserialize(data);
        Debug.Assert(obj != null);
        return (T)obj;
    }
}
namespace LuxonServer;

public static class CustomTypeRegistry
{
    private delegate byte[] Serializer(object value);
    private delegate object Deserializer(ReadOnlySpan<byte> data);

    private static readonly Dictionary<byte, (Serializer, Deserializer)> CustomTypes = [];
    private static readonly Dictionary<Type, byte> CustomTypeCodes = [];

    public static void RegisterType<T>(byte code, Func<T, byte[]> serializer, Func<ReadOnlySpan<byte>, T> deserializer)
        where T : class
    {
        CustomTypes[code] = (
            value => serializer((T)value),
            data => deserializer(data)
        );

        CustomTypeCodes[typeof(T)] = code;
    }

    public static void UnregisterType(byte code)
    {
        CustomTypes.Remove(code);
    }
    internal static byte[] SerializeCustomType(object obj) =>
        !CustomTypeCodes.TryGetValue(obj.GetType(), out var customTypeCode)
        || !CustomTypes.TryGetValue(customTypeCode, out var info)
            ? throw new ArgumentException("Invalid custom type", nameof(obj))
            : info.Item1(obj);

    internal static object DeserializeCustomType(byte code, ReadOnlySpan<byte> data) =>
        !CustomTypes.TryGetValue(code, out var info) 
            ? throw new ArgumentException("Invalid custom type code", nameof(code)) 
            : info.Item2(data);
}
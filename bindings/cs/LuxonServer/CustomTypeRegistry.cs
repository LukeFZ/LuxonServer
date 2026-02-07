namespace LuxonServer;

public static class CustomTypeRegistry
{
    public delegate byte[] SerializeFunction<in T>(T value);
    public delegate T DeserializeFunction<out T>(byte[] data);

    public static void RegisterType<T>(byte code, SerializeFunction<T> serializer, DeserializeFunction<T> deserializer)
    {
        
    }
}
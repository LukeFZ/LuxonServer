namespace LuxonServer;

public static class CustomTypeRegistry
{
    public delegate byte[] SerializeFunction<T>(T value);
    public delegate T DeserializeFunction<T>(byte[] data);

    public static void RegisterType<T>(SerializeFunction<T> serializer, DeserializeFunction<T> deserializer)
    {
        
    }
}
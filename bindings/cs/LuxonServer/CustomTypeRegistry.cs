using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

public static unsafe class CustomTypeRegistry
{
    private delegate byte[] Serializer(ObjectHandle handle);
    private delegate ObjectHandle Deserializer(ReadOnlySpan<byte> data);

    private static readonly Dictionary<byte, (Serializer, Deserializer)> CustomTypes = [];
    private static readonly Dictionary<Type, byte> CustomTypeCodes = [];
    private static bool _registered;

    private static void RegisterCustomTypeRegistry()
    {
        ObjectManager.RegisterObjectManager();

        var customTypeRegistryInterface = new CustomTypeRegistryInterface
        {
            SerializeCustomType = &SerializeCustomType,
            DeserializeCustomType = &DeserializeCustomType,
        };

        NativeMethods.luxon_csharp_set_custom_type_registry(&customTypeRegistryInterface);
    }

    public static void RegisterType<T>(byte code, Func<T, byte[]> serializer, Func<ReadOnlySpan<byte>, T> deserializer)
        where T : class
    {
        if (!_registered)
        {
            RegisterCustomTypeRegistry();
            _registered = true;
        }

        CustomTypes[code] = (
            handle => serializer(handle.ToManagedObject<T>()),
            data => deserializer(data).ToNativeHandle()
        );
        CustomTypeCodes[typeof(T)] = code;

        NativeMethods.luxon_csharp_register_custom_type(code);
    }

    public static void UnregisterType(byte code)
    {
        if (CustomTypes.Remove(code))
        {
            NativeMethods.luxon_csharp_unregister_custom_type(code);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static nuint SerializeCustomType(byte code, ObjectHandle handle, byte** data)
    {
        Debug.Assert(CustomTypes.ContainsKey(code));

        var blob = CustomTypes[code].Item1(handle);
        
        // Copied to unmanaged memory so that we can free it better later
        var buffer = Marshal.AllocHGlobal(blob.Length);
        Marshal.Copy(blob, 0, buffer, blob.Length);
        *data = (byte*)buffer;

        return (nuint)blob.Length;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static ObjectHandle DeserializeCustomType(byte code, byte* data, nuint size)
    {
        Debug.Assert(CustomTypes.ContainsKey(code));

        var span = new ReadOnlySpan<byte>(data, (int)size);
        return CustomTypes[code].Item2(span);
    }

    internal static byte[] SerializeCustomType(object obj) =>
        !CustomTypeCodes.TryGetValue(obj.GetType(), out var customTypeCode)
        || !CustomTypes.TryGetValue(customTypeCode, out var info)
            ? throw new ArgumentException("Invalid custom type", nameof(obj))
            : info.Item1(obj.ToNativeHandle());

    internal static object DeserializeCustomType(byte code, ReadOnlySpan<byte> data) =>
        !CustomTypes.TryGetValue(code, out var info) 
            ? throw new ArgumentException("Invalid custom type code", nameof(code)) 
            : info.Item2(data);
}
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

using ObjectHandle = nint;

public static unsafe class CustomTypeRegistry
{
    private delegate byte[] Serializer(ObjectHandle handle);
    private delegate ObjectHandle Deserializer(ReadOnlySpan<byte> data);

    private static readonly Dictionary<byte, (Serializer, Deserializer)> CustomTypes = new();
    private static bool _registered;

    private static void RegisterCustomTypeRegistry()
    {
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

        NativeMethods.luxon_csharp_register_custom_type(code);
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
}
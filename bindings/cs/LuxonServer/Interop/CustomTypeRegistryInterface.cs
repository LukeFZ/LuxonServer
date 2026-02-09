using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct CustomTypeRegistryInterface
{
    public delegate* unmanaged[Cdecl]<byte, ObjectHandle, byte**, nuint> SerializeCustomType;
    public delegate* unmanaged[Cdecl]<byte, byte*, nuint, ObjectHandle> DeserializeCustomType;

#if DEBUG
    static CustomTypeRegistryInterface()
    {
        Debug.Assert(Unsafe.SizeOf<CustomTypeRegistryInterface>() == sizeof(nuint) * 2);
    }
#endif
}
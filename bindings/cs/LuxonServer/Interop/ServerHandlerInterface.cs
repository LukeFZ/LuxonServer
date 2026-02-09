using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ServerHandlerInterface
{
    private const int FunctionCount = 1;

    public delegate* unmanaged[Cdecl]<ObjectHandle, void> HandleConnect;

#if DEBUG
    static ServerHandlerInterface()
    {
        Debug.Assert(Unsafe.SizeOf<ServerHandlerInterface>() == sizeof(nuint) * FunctionCount);
    }
#endif
}
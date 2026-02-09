using LuxonServer.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ServerHandlerInterface
{
    private const int FunctionCount = 4;

    public delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleConnect;
    public delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleDisconnect;
    public delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleUpdate;
    public delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleSlowUpdate;

#if DEBUG
    static ServerHandlerInterface()
    {
        Debug.Assert(Unsafe.SizeOf<ServerHandlerInterface>() == sizeof(nuint) * FunctionCount);
    }
#endif
}
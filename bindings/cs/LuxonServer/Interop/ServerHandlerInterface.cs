using LuxonServer.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ServerHandlerInterface
{
    private const int FunctionCount = 11;

    public delegate* unmanaged[Cdecl]<ObjectHandle, void> DestroyHandlerInstance;
    public delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleConnect;
    public delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleDisconnect;
    public delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleUpdate;
    public delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleSlowUpdate;
    public void* Reserved0; // ENetConnectionStateChange
    public void* Reserved1; // ENetCommand
    public void* Reserved2; // HTTPRequest
    public void* Reserved3; // InitRequest
    public delegate* unmanaged[Cdecl]<ObjectHandle, NativeOperationRequestMessage*, byte, EnetCommandHeader*, FunctionResult> HandleOperationRequest;
    public void* Reserved5; // InternalOperationRequest

#if DEBUG
    static ServerHandlerInterface()
    {
        Debug.Assert(Unsafe.SizeOf<ServerHandlerInterface>() == sizeof(nuint) * FunctionCount);
    }
#endif
}
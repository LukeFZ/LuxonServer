using System.Runtime.InteropServices;
using LuxonServer.Models;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct InteropInterface
{
    internal delegate* unmanaged[Cdecl]<ObjectHandle, void> FreeObject;

    internal delegate* unmanaged[Cdecl]<byte*, ObjectHandle> CreatePluginInstance;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, void> DestroyPluginInstance;

    internal delegate* unmanaged[Cdecl]<ObjectHandle, PluginResult> OnAttach;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, NativeOperationRequestMessage*, NativeOnCreateGameCallInfo*, PluginResult> OnCreateGame;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, PluginResult> BeforeJoin;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, PluginResult> OnJoinGame;

    internal delegate* unmanaged[Cdecl]<byte*, PeerHandle, ObjectHandle> CreateHandlerInstance;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, void> DestroyHandlerInstance;

    internal delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleConnect;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleDisconnect;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleUpdate;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, FunctionResult> HandleSlowUpdate;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, NativeOperationRequestMessage*, byte, EnetCommandHeader*, FunctionResult> HandleOperationRequest;
}
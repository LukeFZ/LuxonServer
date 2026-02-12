using LuxonServer.Interop;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

// Struct size and layout needs to match with the C++ side

public static unsafe class NativeInterface
{
    private static bool _registered;

    public static void EnsureRegistered()
    {
        if (_registered)
            return;

        var interopInterface = new InteropInterface
        {
            FreeObject = &FreeObject,
            CreatePluginInstance = &PluginManager.CreatePluginInstance,
            DestroyPluginInstance = &PluginManager.DestroyPluginInstance,
            OnAttach = &PluginManager.OnAttach,
            OnCreateGame = &PluginManager.OnCreateGame,
            BeforeJoin = &PluginManager.BeforeJoin,
            OnJoinGame = &PluginManager.OnJoinGame,
            CreateHandlerInstance = &HandlerManager.CreateHandlerInstance,
            DestroyHandlerInstance = &HandlerManager.DestroyHandlerInstance,
            HandleConnect = &HandlerManager.HandleConnect,
            HandleDisconnect = &HandlerManager.HandleDisconnect,
            HandleUpdate = &HandlerManager.HandleUpdate,
            HandleSlowUpdate = &HandlerManager.HandleSlowUpdate,
            HandleOperationRequest = &HandlerManager.HandleOperationRequest
        };

        NativeMethods.luxon_csharp_set_interop_interface(&interopInterface);

        _registered = true;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static void FreeObject(ObjectHandle handle)
    {
        GCHandle.FromIntPtr(handle).Free();
    }
}
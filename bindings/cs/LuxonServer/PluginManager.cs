using LuxonServer.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LuxonServer.Interop;
using LuxonServer.Serialization;

namespace LuxonServer;

public static unsafe class PluginManager
{
    private static readonly Dictionary<string, Func<GamePlugin>> PluginFactories = new();
    private static bool _registered;

    private static void RegisterPluginManager()
    {
        ObjectManager.RegisterObjectManager();

        var managerInterface = new PluginManagerInterface
        {
            CreatePluginInstance = &CreatePluginInstance,
            DestroyPluginInstance = &DestroyPluginInstance,
            OnAttach = &OnAttach,
            OnCreateGame = &OnCreateGame,
            BeforeJoin = &BeforeJoinGame,
            OnJoinGame = &OnJoinGame,
        };

        NativeMethods.luxon_csharp_set_plugin_manager(&managerInterface);
    }

    public static void RegisterPlugin<T>(string name, Func<T> factory) where T : GamePlugin
    {
        if (!_registered)
        {
            RegisterPluginManager();
            _registered = true;
        }

        PluginFactories[name] = factory;

        NativeMethods.luxon_csharp_register_plugin(name);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static ObjectHandle CreatePluginInstance(byte* name)
    {
        var nameStr = Marshal.PtrToStringUTF8((nint)name);
        Debug.Assert(nameStr != null);
        Debug.Assert(PluginFactories.ContainsKey(nameStr));

        var plugin = PluginFactories[nameStr]();

        return plugin.ToNativeHandle();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void DestroyPluginInstance(ObjectHandle handle)
    {
        handle.ToManagedObject<GamePlugin>().Dispose();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult OnAttach(ObjectHandle handle)
    {
        return handle.ToManagedObject<GamePlugin>().OnAttach();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult OnCreateGame(ObjectHandle handle, NativeOperationRequestMessage* message, NativeOnCreateGameCallInfo* info)
    {
        var data = new ReadOnlySpan<byte>(message->SerializedParameters, (int)message->SerializedParametersLength);
        var parsed = VariantSerialization.DeserializeAs<Dictionary<byte, object?>>(data);
        var operationRequestMessage = new OperationRequestMessage(message->OperationCode, parsed);

        var onCreateGameCallInfo = new OnCreateGameCallInfo(info->IsJoin == 1, info->CreateIfNotExist == 1);

        return handle.ToManagedObject<GamePlugin>().OnCreateGame(operationRequestMessage, onCreateGameCallInfo);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult BeforeJoinGame(ObjectHandle handle)
    {
        return handle.ToManagedObject<GamePlugin>().BeforeJoinGame();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult OnJoinGame(ObjectHandle handle)
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult OnLeave(ObjectHandle handle)
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult OnRaiseEvent(ObjectHandle handle)
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult BeforeSetProperties(ObjectHandle handle)
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult OnSetProperties(ObjectHandle handle)
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult BeforeCloseGame(ObjectHandle handle)
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static PluginResult OnCloseGame(ObjectHandle handle)
    {
        return PluginResult.Continue;
    }
}
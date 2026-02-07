using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

using PluginInstanceHandle = ulong;

public static unsafe class PluginManager
{
    private static readonly Dictionary<PluginInstanceHandle, IPlugin> InstantiatedPlugins = new();
    private static readonly Dictionary<string, Func<IPlugin>> PluginFactories = new();
    private static HandleProvider _handleProvider;
    private static bool _registered;

    public static void RegisterPluginManager()
    {
        if (_registered)
            return;

        var managerInterface = new PluginManagerInterface
        {
            CreatePluginInstance = &CreatePluginInstance,
            DestroyPluginInstance = &DestroyPluginInstance,
            OnAttach = &OnAttach,
            OnCreateGame = &OnCreateGame,
        };

        NativeMethods.luxon_csharp_set_plugin_manager(&managerInterface);

        _registered = true;
    }

    public static void RegisterPlugin<T>(string name, Func<T> factory) where T : IPlugin
    {
        PluginFactories[name] = () => factory();

        NativeMethods.luxon_csharp_register_plugin(name);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginInstanceHandle CreatePluginInstance(byte* name)
    {
        var nameStr = Marshal.PtrToStringUTF8((nint)name);
        Debug.Assert(nameStr != null);
        Debug.Assert(PluginFactories.ContainsKey(nameStr));

        var handle = _handleProvider.AcquireHandle();
        var plugin = PluginFactories[nameStr]();
        InstantiatedPlugins[handle] = plugin;

        return handle;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static void DestroyPluginInstance(PluginInstanceHandle handle)
    {
        Debug.Assert(InstantiatedPlugins.ContainsKey(handle));
        if (InstantiatedPlugins.Remove(handle, out var plugin))
        {
            plugin.Dispose();
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult OnAttach(PluginInstanceHandle handle)
    {
        if (InstantiatedPlugins.TryGetValue(handle, out var plugin))
        {
            return plugin.OnAttach();
        }

        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult OnCreateGame(PluginInstanceHandle handle)
    {
        if (InstantiatedPlugins.TryGetValue(handle, out var plugin))
        {
            return plugin.OnAttach();
        }

        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult BeforeJoin()
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult OnJoinGame()
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult OnLeave()
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult OnRaiseEvent()
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult BeforeSetProperties()
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult OnSetProperties()
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult BeforeCloseGame()
    {
        return PluginResult.Continue;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static PluginResult OnCloseGame()
    {
        return PluginResult.Continue;
    }
}
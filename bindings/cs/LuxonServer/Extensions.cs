using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LuxonServer;

public static class Extensions
{
    extension(PluginManager)
    {
        public static void RegisterPlugin<T>(string name) where T : GamePlugin, new()
            => PluginManager.RegisterPlugin(name, () => new T());
    }

    extension<T>(T value)
        where T : class
    {
        public nint ToNativeHandle()
            => GCHandle.ToIntPtr(GCHandle.Alloc(value));
    }

    extension(nint value)
    {
        public T ToManagedObject<T>()
        {
            var handle = GCHandle.FromIntPtr(value);
            Debug.Assert(handle.Target is T);
            return (T)handle.Target;
        }
    }
}
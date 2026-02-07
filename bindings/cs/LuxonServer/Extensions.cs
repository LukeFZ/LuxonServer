namespace LuxonServer;

public static class Extensions
{
    extension(PluginManager)
    {
        public static void RegisterPlugin<T>(string name) where T : IPlugin, new()
            => PluginManager.RegisterPlugin(name, () => new T());
    }
}
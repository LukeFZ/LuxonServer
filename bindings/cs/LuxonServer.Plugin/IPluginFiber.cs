namespace LuxonServer.Plugin;

public interface IPluginFiber
{
    void Enqueue(Action action);
    object CreateTimer(Action action, int firstInMs, int regularInMs);
    object CreateOneTimeTimer(Action action, long firstInMs);
    object CreateOneTimeTimer(Action action, int firstInMs);
    void StopTimer(object timer);
}

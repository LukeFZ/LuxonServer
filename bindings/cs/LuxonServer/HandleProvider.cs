namespace LuxonServer;

public struct HandleProvider
{
    private ulong _nextHandle;

    public ulong AcquireHandle()
        => Interlocked.Increment(ref _nextHandle);
}
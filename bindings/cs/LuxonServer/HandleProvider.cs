namespace LuxonServer;

public struct HandleProvider
{
    private nuint _nextHandle;

    public nuint AcquireHandle()
        => Interlocked.Increment(ref _nextHandle);
}
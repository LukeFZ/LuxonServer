namespace LuxonServer;

public struct HandleProvider
{
    private uint _nextHandle;

    public nuint AcquireHandle()
        => Interlocked.Increment(ref _nextHandle);
}
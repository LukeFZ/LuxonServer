namespace LuxonServer;

public abstract class HandlerBase : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {

    }
}
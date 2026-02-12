namespace LuxonServer.Plugin;

public class CacheOp
{
    public CacheOperations CacheOperation { get; set; }
    public byte Target { get; set; }
    public IList<int> Actors { get; set; } = [];
    public int SliceIndex { get; set; }
    public object? Data { get; set; }
    public int ActorNr { get; set; }
    public byte EventCode { get; set; }

    public CacheOp(CacheOperations cacheOp = 0, byte target = byte.MaxValue)
    {
        CacheOperation = cacheOp;
        Target = target;
    }

    public CacheOp(CacheOperations cacheOp, byte target, byte eventCode, object? data)
    {
        CacheOperation = cacheOp;
        Target = target;
        EventCode = eventCode;
        Data = data;
    }

    public CacheOp(CacheOperations cacheOp, IList<int> actors, byte eventCode, object? data = null)
    {
        CacheOperation = cacheOp;
        Actors = actors;
        EventCode = eventCode;
        Data = data;
    }

    public CacheOp(CacheOperations cacheOp, int sliceIndex, int actor)
    {
        CacheOperation = cacheOp;
        SliceIndex = sliceIndex;
        ActorNr = actor;
    }
}

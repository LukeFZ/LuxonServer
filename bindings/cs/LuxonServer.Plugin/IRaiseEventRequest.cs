namespace LuxonServer.Plugin;

public interface IRaiseEventRequest : IOperationRequest
{
    int[] Actors { get; set; }
    CacheOperations Cache { get; set; }
    object? Data { get; set; }
    byte EvCode { get; set; }
    string GameId { get; set; }
    byte Group { get; set; }
    bool HttpForward { get; set; }
    ReciverGroup ReceiverGroup { get; set; }
    int? CacheSliceIndex { get; set; }
}

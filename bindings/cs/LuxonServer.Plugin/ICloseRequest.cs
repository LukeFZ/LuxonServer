namespace LuxonServer.Plugin;

public interface ICloseRequest : IOperationRequest
{
    int EmptyRoomTTL { get; set; }
}

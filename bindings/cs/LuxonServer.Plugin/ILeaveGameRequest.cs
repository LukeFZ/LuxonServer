namespace LuxonServer.Plugin;

public interface ILeaveGameRequest : IOperationRequest
{
    bool IsCommingBack { get; set; }
}

namespace LuxonServer.Plugin;

public interface ILeaveGameCallInfo : ITypedCallInfo<ILeaveGameRequest>
{
    int ActorNr { get; }
    bool IsInactive { get; }
    LeaveReason Reason { get; }
    string? Details { get; }
}

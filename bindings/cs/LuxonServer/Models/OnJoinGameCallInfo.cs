namespace LuxonServer.Models;

public class OnJoinGameCallInfo
{
    public object Joiner { get; } = null!;
    public bool? PublishUserId { get; set; }
    public bool? BroadcastActorProps { get; set; }
}
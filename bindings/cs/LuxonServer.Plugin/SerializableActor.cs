namespace LuxonServer.Plugin;

public class SerializableActor
{
    public int ActorNr { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public string? Binary { get; set; }
    public DateTime? DeactivationTime { get; set; }
    public Dictionary<byte, object> DEBUG_BINARY { get; set; } = new();
}

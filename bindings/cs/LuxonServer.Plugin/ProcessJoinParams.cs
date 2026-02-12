namespace LuxonServer.Plugin;

public class ProcessJoinParams
{
    public bool PublishCache { get; set; }
    public bool PublishJoinEvents { get; set; }
    public Dictionary<byte, object> ResponseExtraParameters { get; set; } = [];
}

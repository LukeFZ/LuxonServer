namespace LuxonServer.Plugin;

public struct SendParameters
{
    public byte ChannelId { get; set; }
    public bool Encrypted { get; set; }
    public bool Flush { get; set; }
    public bool Unreliable { get; set; }
    public PluginDeliveryMode DeliveryMode { get; set; }
}

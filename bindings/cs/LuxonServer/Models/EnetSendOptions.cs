using System.Runtime.InteropServices;

namespace LuxonServer.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct EnetSendOptions(byte channel = 0)
{
    public byte Channel = channel;
    public EnetDeliveryMode Mode = EnetDeliveryMode.Reliable;
}
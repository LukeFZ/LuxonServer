using System.Runtime.InteropServices;

namespace LuxonServer.Models;

// Must match enet::EnetCommandHeader
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly record struct EnetCommandHeader
{
    public readonly EnetCommandType CommandType;
    public readonly byte ChannelId;
    public readonly byte Flags;
    public readonly byte Reserved;
    public readonly uint CommandLength;
    public readonly uint ReliableSequenceId;
}
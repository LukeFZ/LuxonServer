namespace LuxonServer.Models;

public enum EnetDeliveryMode : uint
{
    Unreliable,
    Reliable,
    UnreliableUnsequenced,
    ReliableUnsequenced
}
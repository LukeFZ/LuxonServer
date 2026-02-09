namespace LuxonServer.Models;

public enum EnetCommandType : byte
{
    None = 0,
    Acknowledge = 1,
    Connect = 2,
    VerifyConnect = 3,
    Disconnect = 4,
    Ping = 5,
    SendReliable = 6,
    SendUnreliable = 7,
    SendFragment = 8,
    SendUnreliableUnsequenced = 11,
    EgServerTime = 12,
    EgSendUnreliableProcessed = 13,
    EgSendReliableUnsequenced = 14,
    EgSendFragmentUnsequenced = 15,
    EgAcknowledgeUnsequenced = 16
}
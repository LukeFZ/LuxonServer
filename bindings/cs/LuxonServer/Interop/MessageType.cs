namespace LuxonServer.Interop;

internal enum MessageType : byte
{
    RawData = 0,
    OperationResponse = 1,
    Event = 2
}
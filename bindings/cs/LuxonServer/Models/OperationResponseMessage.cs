namespace LuxonServer.Models;

public record OperationResponseMessage(byte OperationCode, short ReturnCode, string? DebugMessage, Dictionary<byte, object?> Parameters);
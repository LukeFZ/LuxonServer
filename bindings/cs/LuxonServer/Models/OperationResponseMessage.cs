namespace LuxonServer.Models;

public record OperationResponseMessage(byte OperationCode, short ReturnCode, Dictionary<byte, object?> Parameters, string? DebugMessage = null);
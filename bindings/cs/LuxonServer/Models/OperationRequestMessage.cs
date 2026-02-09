namespace LuxonServer.Models;

public record OperationRequestMessage(byte OperationCode, Dictionary<byte, object?> Parameters);
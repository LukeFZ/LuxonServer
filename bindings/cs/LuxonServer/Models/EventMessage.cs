namespace LuxonServer.Models;

public record EventMessage(byte EventCode, Dictionary<string, object?> Parameters);
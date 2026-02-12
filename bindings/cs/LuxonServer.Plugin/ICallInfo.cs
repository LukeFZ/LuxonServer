using System.Collections.Generic;

namespace LuxonServer.Plugin;

public interface ICallInfo
{
    IOperationRequest OperationRequest { get; }
    byte Status { get; }

    bool IsNew { get; }
    bool IsDeferred { get; }
    bool IsSucceeded { get; }
    bool IsFailed { get; }
    bool IsCanceled { get; }
    bool IsCancelled { get; } // lmao
    bool IsProcessed { get; }
    bool IsPaused { get; }

    void Continue();
    void Fail(string? msg = null, Dictionary<byte, object>? parameters = null);
    void StrictModeCheck(out string errorMessage);
}

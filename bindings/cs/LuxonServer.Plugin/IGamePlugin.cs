using System.Collections.Generic;

namespace LuxonServer.Plugin;

public interface IGamePlugin
{
    string Name { get; }
    string Version { get; }
    bool IsPersistent { get; }

    void BeforeCloseGame(IBeforeCloseGameCallInfo info);
    void BeforeJoin(IBeforeJoinGameCallInfo info);
    void BeforeSetProperties(IBeforeSetPropertiesCallInfo info);
    void OnCloseGame(ICloseGameCallInfo info);
    void OnCreateGame(ICreateGameCallInfo info);
    void OnJoin(IJoinGameCallInfo info);
    void OnLeave(ILeaveGameCallInfo info);
    void OnRaiseEvent(IRaiseEventCallInfo info);
    void OnSetProperties(ISetPropertiesCallInfo info);
    void OnUnknownType(Type type, ref object value);
    bool SetupInstance(IPluginHost host, Dictionary<string, string> config, out string errorMessage);
    void ReportError(ErrorCodes errorCode, Exception e, object? state = null);
}

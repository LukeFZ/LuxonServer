namespace LuxonServer.Plugin;

public class ErrorPlugin : IGamePlugin
{
    public string Name => nameof(ErrorPlugin);
    public string Version => "1.0.0";
    public bool IsPersistent => false;

    public void BeforeCloseGame(IBeforeCloseGameCallInfo info)
    {
        throw new NotImplementedException();
    }

    public void BeforeJoin(IBeforeJoinGameCallInfo info)
    {
        throw new NotImplementedException();
    }

    public void BeforeSetProperties(IBeforeSetPropertiesCallInfo info)
    {
        throw new NotImplementedException();
    }

    public void OnCloseGame(ICloseGameCallInfo info)
    {
        throw new NotImplementedException();
    }

    public void OnCreateGame(ICreateGameCallInfo info)
    {
        throw new NotImplementedException();
    }

    public void OnJoin(IJoinGameCallInfo info)
    {
        throw new NotImplementedException();
    }

    public void OnLeave(ILeaveGameCallInfo info)
    {
        throw new NotImplementedException();
    }

    public void OnRaiseEvent(IRaiseEventCallInfo info)
    {
        throw new NotImplementedException();
    }

    public void OnSetProperties(ISetPropertiesCallInfo info)
    {
        throw new NotImplementedException();
    }

    public void OnUnknownType(Type type, ref object value)
    {
        throw new NotImplementedException();
    }

    public bool SetupInstance(IPluginHost host, Dictionary<string, string> config, out string errorMessage)
    {
        throw new NotImplementedException();
    }

    public void ReportError(ErrorCodes errorCode, Exception e, object? state = null)
    {
        throw new NotImplementedException();
    }
}

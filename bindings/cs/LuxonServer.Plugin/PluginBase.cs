namespace LuxonServer.Plugin;

public abstract class PluginBase : IGamePlugin
{
    public static readonly Version PluginsVersion = new(1, 2);
    public static readonly Version BuildVersion = new();

    public virtual string Name { get; } = "";
    public virtual string Version { get; } = "";
    public virtual bool IsPersistent { get; }

    public IPluginHost? Host { get; protected set; }
    public string AppVersion { get; } = "";
    public string AppId { get; } = "";
    public string Region { get; } = "";
    public string Cloud { get; } = "";
    public string EnvironmentVerion { get; } = ""; // typo x3

    protected bool fireAssert = true;

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

    protected virtual void OnChangeMasterClientId(int oldId, int newId)
    {

    }

    protected virtual void ReportError(short errorCode, Exception exception, object? state)
    {

    }

    protected virtual void StrictModeCheck(ICallInfo info)
    {

    }

    protected void BroadcastEvent(byte code, Dictionary<byte, object?> data)
    {

    }
}

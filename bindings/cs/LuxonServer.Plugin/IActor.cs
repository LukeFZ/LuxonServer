namespace LuxonServer.Plugin;

public interface IActor
{
    int ActorNr { get; }
    PropertyBag<object> Properties { get; }
    string UserId { get; }
    string Nickname { get; }
    bool IsActive { get; }
    object? Secure { get; }

    void UpdateSecure(string key, object? value);
}

namespace LuxonServer.Plugin;

public class PropertyChangedEventArgs<TKey>(TKey key, object? value) : EventArgs
{
    public TKey Key { get; } = key;
    public object? Value { get; } = value;
}

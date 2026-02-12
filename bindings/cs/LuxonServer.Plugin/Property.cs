namespace LuxonServer.Plugin;

public class Property<TKey>
{
    public TKey Key { get; }
    public int KeySize { get; }

    public object? Value { get; set; }
    public int ValueSize { get; set; }

    public int TotalSize => KeySize + ValueSize;

    public event EventHandler? PropertyChanged;

    public Property(TKey key, object? value) : this(key, value, 0, 0)
    {
    }

    public Property(TKey key, object? value, int keySize, int valueSize)
    {
        Key = key;
        Value = value;
        KeySize = keySize;
        ValueSize = valueSize;
    }
}

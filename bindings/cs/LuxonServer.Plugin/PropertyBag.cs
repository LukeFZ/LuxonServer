namespace LuxonServer.Plugin;

public class PropertyBag<TKey>
{
    public int Count => 0;
    public bool DeleteNullProps { get; set; }
    public int TotalSize { get; }

    public event EventHandler<PropertyChangedEventArgs<TKey>>? PropertyChanged; 
}

using System.Runtime.CompilerServices;
using System.Text;

namespace LuxonServer.Serialization;

public ref struct Serializer()
{
    public byte[] Buffer  => _buffer;

    private byte[] _buffer = new byte[16];
    private int _offset;

    private void EnsureCapacity(int additionalSize)
    {
        var requiredSize = _offset + additionalSize;
        if (requiredSize > _buffer.Length)
        {
            var newSize = Math.Max(_buffer.Length * 2, requiredSize);
            Array.Resize(ref _buffer, newSize);
        }
    }

    private Span<byte> GetBytes(int size)
    {
        EnsureCapacity(size);
        var span = _buffer.AsSpan(_offset, size);
        _offset += size;
        return span;
    }

    private void WritePrimitive<T>(T value)
        where T : unmanaged
    {
        var size = Unsafe.SizeOf<T>();
        EnsureCapacity(size);
        Unsafe.WriteUnaligned(ref _buffer[_offset], value);
        _offset += size;
    }

    private void WriteString(string value)
    {
        var data = Encoding.UTF8.GetByteCount(value);
        WritePrimitive((nuint)data);
        Encoding.UTF8.GetBytes(value, GetBytes(data));
    }

    private void WritePrimitiveArray<T>(T[] array)
        where T : unmanaged
    {
        WritePrimitive((nuint)array.Length);
        foreach (var item in array)
        {
            WritePrimitive(item);
        }
    }

    private void WriteStringArray(string[] array)
    {
        WritePrimitive((nuint)array.Length);
        foreach (var item in array)
        {
            WriteString(item);
        }
    }

    private void WriteObjectArray(object?[] array)
    {
        WritePrimitive((nuint)array.Length);
        foreach (var item in array)
        {
            WriteObject(item);
        }
    }

    private void WriteDictionary(Dictionary<byte, object?> dict)
    {
        WritePrimitive((nuint)dict.Count);
        foreach (var kvp in dict)
        {
            WritePrimitive(kvp.Key);
            WriteObject(kvp.Value);
        }
    }

    private void WriteHashtable(Dictionary<object, object?> dict)
    {
        WritePrimitive((nuint)dict.Count);
        foreach (var kvp in dict)
        {
            WriteObject(kvp.Key);
            WriteObject(kvp.Value);
        }
    }

    private void WriteCustomValue(object value)
    {
        var data = CustomTypeRegistry.SerializeCustomType(value);
        data.CopyTo(GetBytes(data.Length));
    }

    public void WriteObject(object? value)
    {
        switch (value)
        {
            case null:
                WritePrimitive(VariantValueType.Null);
                break;
            case bool boolean:
                WritePrimitive(VariantValueType.Boolean);
                WritePrimitive(boolean);
                break;
            case byte byt:
                WritePrimitive(VariantValueType.Byte);
                WritePrimitive(byt);
                break;
            case short val:
                WritePrimitive(VariantValueType.Short);
                WritePrimitive(val);
                break;
            case int val:
                WritePrimitive(VariantValueType.Int);
                WritePrimitive(val);
                break;
            case long val:
                WritePrimitive(VariantValueType.Long);
                WritePrimitive(val);
                break;
            case float val:
                WritePrimitive(VariantValueType.Float);
                WritePrimitive(val);
                break;
            case double val:
                WritePrimitive(VariantValueType.Double);
                WritePrimitive(val);
                break;
            case string val:
                WritePrimitive(VariantValueType.String);
                WriteString(val);
                break;
            case bool[] val:
                WritePrimitive(VariantValueType.BooleanArray);
                WritePrimitiveArray(val);
                break;
            case byte[] val:
                WritePrimitive(VariantValueType.ByteArray);
                WritePrimitiveArray(val);
                break;
            case short[] val:
                WritePrimitive(VariantValueType.ShortArray);
                WritePrimitiveArray(val);
                break;
            case int[] val:
                WritePrimitive(VariantValueType.IntArray);
                WritePrimitiveArray(val);
                break;
            case long[] val:
                WritePrimitive(VariantValueType.LongArray);
                WritePrimitiveArray(val);
                break;
            case float[] val:
                WritePrimitive(VariantValueType.FloatArray);
                WritePrimitiveArray(val);
                break;
            case double[] val:
                WritePrimitive(VariantValueType.DoubleArray);
                WritePrimitiveArray(val);
                break;
            case string[] val:
                WritePrimitive(VariantValueType.StringArray);
                WriteStringArray(val);
                break;
            case Dictionary<byte, object?> val:
                WritePrimitive(VariantValueType.Dictionary);
                WriteDictionary(val);
                break;
            case Dictionary<object, object?> val:
                WritePrimitive(VariantValueType.Hashtable);
                WriteHashtable(val);
                break;
            case object?[] val:
                WritePrimitive(VariantValueType.ObjectArray);
                WriteObjectArray(val);
                break;
            default:
                WritePrimitive(VariantValueType.CustomValue);
                WriteCustomValue(value);
                break;
        }
    }
}
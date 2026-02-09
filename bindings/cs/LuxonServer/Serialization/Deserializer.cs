using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace LuxonServer.Serialization;

internal ref struct Deserializer(ReadOnlySpan<byte> data)
{
    private readonly ReadOnlySpan<byte> _data = data;
    private int _offset;

    private ReadOnlySpan<byte> ReadBytes(int size)
    {
        var slice = _data.Slice(_offset, size);
        _offset += size;
        return slice;
    }

    private T ReadPrimitive<T>()
        where T : unmanaged
    {

        var data = ReadBytes(Unsafe.SizeOf<T>());
        return MemoryMarshal.Read<T>(data);
    }

    private string ReadString()
    {
        var size = ReadPrimitive<nuint>();
        var data = ReadBytes((int)size);
        return Encoding.UTF8.GetString(data);
    }

    private T[] ReadPrimitiveArray<T>()
        where T : unmanaged
    {
        var count = ReadPrimitive<nuint>();

        var array = new T[count];
        for (nuint i = 0; i < count; i++)
        {
            array[i] = ReadPrimitive<T>();
        }

        return array;
    }

    private string[] ReadStringArray()
    {
        var count = ReadPrimitive<nuint>();

        var array = new string[count];
        for (nuint i = 0; i < count; i++)
        {
            array[i] = ReadString();
        }

        return array;
    }

    private object?[] ReadObjectArray()
    {
        var count = ReadPrimitive<nuint>();

        var array = new object?[count];
        for (nuint i = 0; i < count; i++)
        {
            array[i] = ReadObject();
        }

        return array;
    }

    private Dictionary<byte, object?> ReadDictionary()
    {
        var count = ReadPrimitive<nuint>();

        var dict = new Dictionary<byte, object?>();
        dict.EnsureCapacity((int)count);

        for (nuint i = 0; i < count; i++)
        {
            var key = ReadPrimitive<byte>();
            var value = ReadObject();
            dict[key] = value;
        }

        return dict;
    }

    private Dictionary<object, object?> ReadHashtable()
    {
        var count = ReadPrimitive<nuint>();

        var dict = new Dictionary<object, object?>();
        dict.EnsureCapacity((int)count);

        for (nuint i = 0; i < count; i++)
        {
            var key = ReadObject()!;
            var value = ReadObject();
            dict[key] = value;
        }

        return dict;
    }

    private object ReadCustomValue()
    {
        var code = ReadPrimitive<byte>();
        var size = ReadPrimitive<nuint>();
        var data = ReadBytes((int)size);
        return CustomTypeRegistry.DeserializeCustomType(code, data);
    }

    private object? ReadValue(ValueType type)
    {
        switch (type)
        {
            case VariantValueType.Null:
                return null;
            case VariantValueType.Boolean:
                return ReadPrimitive<bool>();
            case VariantValueType.Byte:
                return ReadPrimitive<byte>();
            case VariantValueType.Short:
                return ReadPrimitive<short>();
            case VariantValueType.Int:
                return ReadPrimitive<int>();
            case VariantValueType.Long:
                return ReadPrimitive<long>();
            case VariantValueType.Float:
                return ReadPrimitive<float>();
            case VariantValueType.Double:
                return ReadPrimitive<double>();
            case VariantValueType.String:
                return ReadString();
            case VariantValueType.ObjectArray:
                return ReadObjectArray();
            case VariantValueType.BooleanArray:
                return ReadPrimitiveArray<bool>();
            case VariantValueType.ByteArray:
                return ReadPrimitiveArray<byte>();
            case VariantValueType.ShortArray:
                return ReadPrimitiveArray<short>();
            case VariantValueType.IntArray:
                return ReadPrimitiveArray<int>();
            case VariantValueType.LongArray:
                return ReadPrimitiveArray<long>();
            case VariantValueType.FloatArray:
                return ReadPrimitiveArray<float>();
            case VariantValueType.DoubleArray:
                return ReadPrimitiveArray<double>();
            case VariantValueType.StringArray:
                return ReadStringArray();
            case VariantValueType.Dictionary:
                return ReadDictionary();
            case VariantValueType.Hashtable:
                return ReadHashtable();
            case VariantValueType.CustomValue:
                return ReadCustomValue();
            default:
                throw new UnreachableException();
        }
    }

    public object? ReadObject()
        => ReadValue(ReadPrimitive<VariantValueType>());
}
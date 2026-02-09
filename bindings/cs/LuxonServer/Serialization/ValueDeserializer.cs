using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace LuxonServer.Serialization;

internal ref struct ValueDeserializer(ReadOnlySpan<byte> data)
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
        var size = ReadPrimitive<ulong>();
        var data = ReadBytes((int)size);
        return Encoding.UTF8.GetString(data);
    }

    private T[] ReadPrimitiveArray<T>()
        where T : unmanaged
    {
        var count = ReadPrimitive<ulong>();

        var array = new T[count];
        for (nuint i = 0; i < count; i++)
        {
            array[i] = ReadPrimitive<T>();
        }

        return array;
    }

    private string[] ReadStringArray()
    {
        var count = ReadPrimitive<ulong>();

        var array = new string[count];
        for (nuint i = 0; i < count; i++)
        {
            array[i] = ReadString();
        }

        return array;
    }

    private object?[] ReadObjectArray()
    {
        var count = ReadPrimitive<ulong>();

        var array = new object?[count];
        for (nuint i = 0; i < count; i++)
        {
            array[i] = ReadObject();
        }

        return array;
    }

    private Dictionary<byte, object?> ReadDictionary()
    {
        var count = ReadPrimitive<ulong>();

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
        var count = ReadPrimitive<ulong>();

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
        var size = ReadPrimitive<ulong>();
        var data = ReadBytes((int)size);
        return CustomTypeRegistry.DeserializeCustomType(code, data);
    }

    private object? ReadValue(ValueType type)
    {
        switch (type)
        {
            case ValueTypeId.Null:
                return null;
            case ValueTypeId.Boolean:
                return ReadPrimitive<bool>();
            case ValueTypeId.Byte:
                return ReadPrimitive<byte>();
            case ValueTypeId.Short:
                return ReadPrimitive<short>();
            case ValueTypeId.Int:
                return ReadPrimitive<int>();
            case ValueTypeId.Long:
                return ReadPrimitive<long>();
            case ValueTypeId.Float:
                return ReadPrimitive<float>();
            case ValueTypeId.Double:
                return ReadPrimitive<double>();
            case ValueTypeId.String:
                return ReadString();
            case ValueTypeId.ObjectArray:
                return ReadObjectArray();
            case ValueTypeId.BooleanArray:
                return ReadPrimitiveArray<bool>();
            case ValueTypeId.ByteArray:
                return ReadPrimitiveArray<byte>();
            case ValueTypeId.ShortArray:
                return ReadPrimitiveArray<short>();
            case ValueTypeId.IntArray:
                return ReadPrimitiveArray<int>();
            case ValueTypeId.LongArray:
                return ReadPrimitiveArray<long>();
            case ValueTypeId.FloatArray:
                return ReadPrimitiveArray<float>();
            case ValueTypeId.DoubleArray:
                return ReadPrimitiveArray<double>();
            case ValueTypeId.StringArray:
                return ReadStringArray();
            case ValueTypeId.Dictionary:
                return ReadDictionary();
            case ValueTypeId.Hashtable:
                return ReadHashtable();
            case ValueTypeId.CustomValue:
                return ReadCustomValue();
            default:
                throw new UnreachableException();
        }
    }

    public object? ReadObject()
        => ReadValue(ReadPrimitive<ValueTypeId>());
}
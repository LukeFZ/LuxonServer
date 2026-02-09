namespace LuxonServer.Serialization;

file static class ValueTypeIds
{
    public const byte IsArrayFlag = 0x80;
}

public enum ValueTypeId : byte
{
    Null,
    Boolean,
    Byte,
    Short,
    Int,
    Long,
    Float,
    Double,
    String,
    Dictionary,
    Hashtable,
    CustomValue,

    ObjectArray = Null | ValueTypeIds.IsArrayFlag,
    BooleanArray = Boolean | ValueTypeIds.IsArrayFlag,
    ByteArray = Byte | ValueTypeIds.IsArrayFlag,
    ShortArray = Short | ValueTypeIds.IsArrayFlag,
    IntArray = Int | ValueTypeIds.IsArrayFlag,
    LongArray = Long | ValueTypeIds.IsArrayFlag,
    FloatArray = Float | ValueTypeIds.IsArrayFlag,
    DoubleArray = Double | ValueTypeIds.IsArrayFlag,
    StringArray = String | ValueTypeIds.IsArrayFlag
}
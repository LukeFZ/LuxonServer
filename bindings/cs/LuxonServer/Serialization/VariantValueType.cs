namespace LuxonServer.Serialization;

file static class VariantValueTypeExtensions
{
    public const byte IsArrayFlag = 0x80;
}

public enum VariantValueType : byte
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

    ObjectArray = Null | VariantValueTypeExtensions.IsArrayFlag,
    BooleanArray = Boolean | VariantValueTypeExtensions.IsArrayFlag,
    ByteArray = Byte | VariantValueTypeExtensions.IsArrayFlag,
    ShortArray = Short | VariantValueTypeExtensions.IsArrayFlag,
    IntArray = Int | VariantValueTypeExtensions.IsArrayFlag,
    LongArray = Long | VariantValueTypeExtensions.IsArrayFlag,
    FloatArray = Float | VariantValueTypeExtensions.IsArrayFlag,
    DoubleArray = Double | VariantValueTypeExtensions.IsArrayFlag,
    StringArray = String | VariantValueTypeExtensions.IsArrayFlag
}
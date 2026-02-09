#pragma once
#include <cstdint>
#include <vector>

#include "luxon/ser_types.hpp"

constexpr auto kIsArrayFlag = 0x80;

enum class ValueType : uint8_t {
    Null = 0,
    Boolean = 1,
    Byte = 2,
    Short = 3,
    Int = 4,
    Long = 5,
    Float = 6,
    Double = 7,
    String = 8,
    Dictionary = 9,
    Hashtable = 10,
    CustomValue = 11,

    ObjectArray = ValueType::Null | kIsArrayFlag,
    BooleanArray = ValueType::Boolean | kIsArrayFlag,
    ByteArray = ValueType::Byte | kIsArrayFlag,
    ShortArray = ValueType::Short | kIsArrayFlag,
    IntArray = ValueType::Int | kIsArrayFlag,
    LongArray = ValueType::Long | kIsArrayFlag,
    FloatArray = ValueType::Float | kIsArrayFlag,
    DoubleArray = ValueType::Double | kIsArrayFlag,
    StringArray = ValueType::String | kIsArrayFlag,
};

// Fast serialization format for marshalling variants.
// Since this is only meant to be used locally, size is not the main concern.

using SerializedVariant = std::vector<uint8_t>;

SerializedVariant serialize_variant(const luxon::ser::Value& value);
luxon::ser::Value deserialize_variant(const luxon::ser::ByteArray& data);

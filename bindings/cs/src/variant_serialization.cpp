#include "variant_serialization.hpp"

#include <memory>
#include <vector>
#include <spanstream>

#include <luxon/ser_buffer.hpp>

#include "custom_type.hpp"

namespace {
void serialize_value(luxon::ser::ByteWriter& writer, const luxon::ser::Value& value) {
    std::visit(
        [&writer]<typename T>(T& element) {
            using VariantValue = std::decay_t<T>;

            if constexpr (std::is_same_v<VariantValue, std::monostate>) {
                writer.write_u8(std::to_underlying(ValueType::Null));
            } else if constexpr (std::is_same_v<VariantValue, bool>) {
                writer.write_u8(std::to_underlying(ValueType::Boolean));
                writer.write_u8(element ? 1 : 0);
            } else if constexpr (std::is_same_v<VariantValue, uint8_t>) {
                writer.write_u8(std::to_underlying(ValueType::Byte));
                writer.write_u8(element);
            } else if constexpr (std::is_same_v<VariantValue, int16_t>) {
                writer.write_u8(std::to_underlying(ValueType::Short));
                writer.write_i16_le(element);
            } else if constexpr (std::is_same_v<VariantValue, int32_t>) {
                writer.write_u8(std::to_underlying(ValueType::Int));
                writer.write_i32_le(element);
            } else if constexpr (std::is_same_v<VariantValue, int64_t>) {
                writer.write_u8(std::to_underlying(ValueType::Long));
                writer.write_i64_le(element);
            } else if constexpr (std::is_same_v<VariantValue, float>) {
                writer.write_u8(std::to_underlying(ValueType::Float));
                writer.write_f32_le(element);
            } else if constexpr (std::is_same_v<VariantValue, double>) {
                writer.write_u8(std::to_underlying(ValueType::Double));
                writer.write_f64_le(element);
            } else if constexpr (std::is_same_v<VariantValue, std::string>) {
                writer.write_u8(std::to_underlying(ValueType::String));
                writer.write_u64_le(element.size());
                writer.write_bytes(std::span(reinterpret_cast<const uint8_t *>(element.data()), element.size()));
            } else if constexpr (std::is_same_v<VariantValue, luxon::ser::ObjectArray>) {
                writer.write_u8(std::to_underlying(ValueType::ObjectArray));
                writer.write_u64_le(element.size());
                for (const auto& item : element) {
                    serialize_value(writer, item);
                }
            } else if constexpr (std::is_same_v<VariantValue, luxon::ser::Dictionary>) {
                writer.write_u8(std::to_underlying(ValueType::Dictionary));
                writer.write_u64_le(element.size());
                for (const auto& [key, val] : element) {
                    writer.write_u8(key);
                    serialize_value(writer, val);
                }
            } else if constexpr (std::is_same_v<VariantValue, luxon::ser::Hashtable>) {
                writer.write_u8(std::to_underlying(ValueType::Hashtable));
                writer.write_u64_le(element.size());
                for (const auto& [key, val] : element) {
                    serialize_value(writer, key);
                    serialize_value(writer, val);
                }
            } else if constexpr (std::is_same_v<VariantValue, luxon::ser::RawCustomValue>) {
                writer.write_u8(std::to_underlying(ValueType::CustomValue));
                writer.write_u8(element.custom_code);
                writer.write_u64_le(element.data.size());
                writer.write_bytes(element.data);
            } else if constexpr (std::is_same_v<VariantValue, std::vector<bool>>) {
                writer.write_u8(std::to_underlying(ValueType::BooleanArray));
                for (const auto& val : element)
                    writer.write_u8(val);
            } else if constexpr (std::is_same_v<VariantValue, std::vector<uint8_t>>) {
                writer.write_u8(std::to_underlying(ValueType::ByteArray));
                for (const auto& val : element)
                    writer.write_u8(val);
            } else if constexpr (std::is_same_v<VariantValue, std::vector<int16_t>>) {
                writer.write_u8(std::to_underlying(ValueType::ShortArray));
                for (const auto& val : element)
                    writer.write_i16_le(val);
            } else if constexpr (std::is_same_v<VariantValue, std::vector<int32_t>>) {
                writer.write_u8(std::to_underlying(ValueType::IntArray));
                for (const auto& val : element)
                    writer.write_i32_le(val);
            } else if constexpr (std::is_same_v<VariantValue, std::vector<int64_t>>) {
                writer.write_u8(std::to_underlying(ValueType::LongArray));
                for (const auto& val : element)
                    writer.write_i64_le(val);
            } else if constexpr (std::is_same_v<VariantValue, std::vector<float>>) {
                writer.write_u8(std::to_underlying(ValueType::FloatArray));
                for (const auto& val : element)
                    writer.write_f32_le(val);
            } else if constexpr (std::is_same_v<VariantValue, std::vector<double>>) {
                writer.write_u8(std::to_underlying(ValueType::DoubleArray));
                for (const auto& val : element)
                    writer.write_f64_le(val);
            }
        },
        value.value);
}

luxon::ser::Value deserialize_value(luxon::ser::ByteReader& reader) {
    switch (static_cast<ValueType>(reader.read_u8().value())) {
        case ValueType::Null:
            return std::monostate{};
        case ValueType::Boolean:
            return reader.read_u8().value() != 0;
        case ValueType::Byte:
            return reader.read_u8().value();
        case ValueType::Short:
            return reader.read_i16_le().value();
        case ValueType::Int:
            return reader.read_i32_le().value();
        case ValueType::Long:
            return reader.read_i64_le().value();
        case ValueType::Float:
            return reader.read_f32_le().value();
        case ValueType::Double:
            return reader.read_f64_le().value();
        case ValueType::String: {
            const auto size = reader.read_u64_le().value();
            const auto bytes = reader.read_span(size).value();
            return std::string(reinterpret_cast<const char *>(bytes.data()), bytes.size());
        }
        case ValueType::ObjectArray: {
            const auto size = reader.read_u64_le().value();
            luxon::ser::ObjectArray arr;
            arr.reserve(size);
            for (uint64_t i = 0; i < size; ++i) {
                arr.push_back(deserialize_value(reader));
            }
            return arr;
        }
        case ValueType::Dictionary: {
            const auto size = reader.read_u64_le().value();
            luxon::ser::Dictionary dict;
            for (uint64_t i = 0; i < size; ++i) {
                const auto key = reader.read_u8().value();
                const auto val = deserialize_value(reader);
                dict.emplace(key, val);
            }
            return dict;
        }
        case ValueType::Hashtable: {
            const auto size = reader.read_u64_le().value();
            auto table = std::make_shared<luxon::ser::Hashtable>();
            for (uint64_t i = 0; i < size; ++i) {
                const auto key = deserialize_value(reader);
                const auto val = deserialize_value(reader);
                table->emplace(key, val);
            }
            return table;
        }
        case ValueType::CustomValue: {
            const auto custom_code = reader.read_u8().value();
            const auto size = reader.read_u64_le().value();
            const auto bytes = reader.read_span(size).value();
            return luxon::ser::RawCustomValue{.custom_code = custom_code, .data = luxon::ser::ByteArray(bytes.begin(), bytes.end())};
        }
        case ValueType::BooleanArray: {
            const auto size = reader.read_u64_le().value();
            std::vector<bool> arr;
            arr.reserve(size);
            for (size_t i = 0; i < size; ++i) {
                arr.push_back(reader.read_u8().value() != 0);
            }
            return arr;
        }
        case ValueType::ByteArray: {
            const auto size = reader.read_u64_le().value();
            const auto bytes = reader.read_span(size).value();
            return luxon::ser::ByteArray(bytes.begin(), bytes.end());
        }
        case ValueType::ShortArray: {
            const auto size = reader.read_u64_le().value();
            std::vector<int16_t> arr;
            arr.reserve(size);
            for (size_t i = 0; i < size; ++i) {
                arr.push_back(reader.read_i16_le().value());
            }
            return arr;
        }
        case ValueType::IntArray: {
            const auto size = reader.read_u64_le().value();
            std::vector<int32_t> arr;
            arr.reserve(size);
            for (size_t i = 0; i < size; ++i) {
                arr.push_back(reader.read_i32_le().value());
            }
            return arr;
        }
        case ValueType::LongArray: {
            const auto size = reader.read_u64_le().value();
            std::vector<int64_t> arr;
            arr.reserve(size);
            for (size_t i = 0; i < size; ++i) {
                arr.push_back(reader.read_i64_le().value());
            }
            return arr;
        }
        case ValueType::FloatArray: {
            const auto size = reader.read_u64_le().value();
            std::vector<float> arr;
            arr.reserve(size);
            for (size_t i = 0; i < size; ++i) {
                arr.push_back(reader.read_f32_le().value());
            }
            return arr;
        }
        case ValueType::DoubleArray: {
            const auto size = reader.read_u64_le().value();
            std::vector<double> arr;
            arr.reserve(size);
            for (size_t i = 0; i < size; ++i) {
                arr.push_back(reader.read_f64_le().value());
            }
            return arr;
        }
        case ValueType::StringArray: {
            const auto size = reader.read_u64_le().value();
            std::vector<std::string> arr;
            arr.reserve(size);
            for (size_t i = 0; i < size; ++i) {
                const auto count = reader.read_u64_le().value();
                const auto bytes = reader.read_span(count).value();
                arr.emplace_back(reinterpret_cast<const char *>(bytes.data()), bytes.size());
            }
            return arr;
        }
    }

    throw std::out_of_range("Invalid value type");
}
} // namespace

luxon::ser::ByteArray serialize_variant(const luxon::ser::Value& value) {
    luxon::ser::ByteWriter writer;
    serialize_value(writer, value);
    return writer.take();
}

luxon::ser::Value deserialize_variant(const luxon::ser::ByteArray& data) {
    luxon::ser::ByteReader reader(data);
    return deserialize_value(reader);
}

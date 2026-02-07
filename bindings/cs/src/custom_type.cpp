#include <span>
#include <stdexcept>

#include "export.hpp"
#include "luxon/ser_types.hpp"

// Opaque handle for an instance of a custom object
using CustomObjectHandle = uintptr_t;

struct CustomObject {
    CustomObjectHandle handle;
    uint8_t code;
};

using CSharpSerializerFunction = void(*)(CustomObjectHandle handle, uint8_t** out_buffer, size_t* out_size);
using CSharpDeserializerFunction = CustomObjectHandle(*)(const uint8_t* buffer, size_t size);

CSHARP_API void luxon_csharp_register_custom_type(const uint8_t code, CSharpSerializerFunction serializer, CSharpDeserializerFunction deserializer) {
    
}

CSHARP_API void luxon_csharp_unregister_custom_type(const uint8_t code) {
    
}

class CustomTypeRegistry {
public:
    luxon::ser::RawCustomValue serialize_object(const CustomObject& object) {
        const auto info = registered_type_map_.find(object.code);

        if (info == registered_type_map_.end()) {
            throw std::out_of_range("Tried to serialize unknown custom value with code " + std::to_string(object.code));
        }

        uint8_t *buffer = nullptr;
        size_t size = 0;
        info->second.serializer(object.handle, &buffer, &size);

        return luxon::ser::RawCustomValue{.custom_code = object.code, .data = luxon::ser::ByteArray(buffer, buffer + size)};
    }

    CustomObject deserialize_object(const luxon::ser::RawCustomValue& value) {
        const auto info = registered_type_map_.find(value.custom_code);

        if (info == registered_type_map_.end()) {
            throw std::out_of_range("Tried to deserialize unknown custom value with code " + std::to_string(value.custom_code));
        }

        const auto handle = info->second.deserializer(value.data.data(), value.data.size());
        return CustomObject{.handle = handle, .code = value.custom_code};
    }
private:
    struct SerializationInfo {
        CSharpSerializerFunction serializer;
        CSharpDeserializerFunction deserializer;
    };

    std::unordered_map<uint8_t, SerializationInfo> registered_type_map_;
};
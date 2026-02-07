#pragma once

#include <cstdint>
#include <unordered_set>

#include "object_management.hpp"
#include "luxon/ser_types.hpp"

struct ParsedCustomValue {
    uint8_t custom_code;
    std::shared_ptr<ManagedObject> managed_object;
};

// Struct size and layout needs to match with the C# side
struct CustomTypeRegistryInterface {
    size_t (*serialize_custom_type)(uint8_t custom_code, ObjectHandle handle, uint8_t **out_buffer);
    ObjectHandle (*deserialize_custom_type)(uint8_t custom_code, const uint8_t *buffer, size_t size);
};
static_assert(sizeof(CustomTypeRegistryInterface) == sizeof(uintptr_t) * 2);

class CustomTypeRegistry {
public:
    static void set_interface(const CustomTypeRegistryInterface *interface);

    static void register_custom_type(uint8_t code);
    static void unregister_custom_type(uint8_t code);
    static bool is_registered(uint8_t code);

    static luxon::ser::RawCustomValue serialize_value(const ParsedCustomValue& value);
    static ParsedCustomValue deserialize_value(const luxon::ser::RawCustomValue& value);
private:
    static std::unordered_set<uint8_t> registered_codes_;
    static CustomTypeRegistryInterface interface_;
};

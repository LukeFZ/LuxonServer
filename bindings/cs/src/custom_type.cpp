#include "custom_type.hpp"
#include "export.hpp"

#include <unordered_set>

namespace {
std::unordered_set<uint8_t> registered_codes;
CustomTypeRegistryInterface custom_type_registry_interface;
} // namespace

CSHARP_API void luxon_csharp_register_custom_type(const uint8_t code) { CustomTypeRegistry::register_custom_type(code); }

CSHARP_API void luxon_csharp_unregister_custom_type(const uint8_t code) { CustomTypeRegistry::unregister_custom_type(code); }

CSHARP_API void luxon_csharp_set_custom_type_registry(const CustomTypeRegistryInterface *interface) { custom_type_registry_interface = *interface; }

void CustomTypeRegistry::register_custom_type(const uint8_t code) { registered_codes.emplace(code); }

void CustomTypeRegistry::unregister_custom_type(const uint8_t code) { registered_codes.erase(code); }

bool CustomTypeRegistry::is_registered(const uint8_t code) { return registered_codes.contains(code); }

luxon::ser::RawCustomValue CustomTypeRegistry::serialize_value(const ParsedCustomValue& value) {
    uint8_t *buffer = nullptr;
    const auto size = custom_type_registry_interface.serialize_custom_type(value.custom_code, value.managed_object->handle(), &buffer);

    const auto allocation = ManagedAllocation(buffer, size);
    const auto span = allocation.span();

    return luxon::ser::RawCustomValue{.custom_code = value.custom_code, .data = luxon::ser::ByteArray(span.begin(), span.end())};
}

ParsedCustomValue CustomTypeRegistry::deserialize_value(const luxon::ser::RawCustomValue& value) {
    const auto handle = custom_type_registry_interface.deserialize_custom_type(value.custom_code, value.data.data(), value.data.size());
    return ParsedCustomValue{.custom_code = value.custom_code, .managed_object = std::make_shared<ManagedObject>(handle)};
}

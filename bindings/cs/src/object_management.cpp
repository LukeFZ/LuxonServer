#include "object_management.hpp"

#include "export.hpp"

namespace {
ObjectManagerInterface object_manager_interface;
}

CSHARP_API void luxon_csharp_set_object_manager(const ObjectManagerInterface *interface) { object_manager_interface = *interface; }

ManagedObject::ManagedObject(const ObjectHandle handle) : handle_(handle) {}

ManagedObject::~ManagedObject() { object_manager_interface.destroy_object(handle_); }

ManagedAllocation::ManagedAllocation(uint8_t *allocation, const size_t size) : allocation_(allocation), size_(size) {}

ManagedAllocation::~ManagedAllocation() { object_manager_interface.free_allocation(allocation_); }

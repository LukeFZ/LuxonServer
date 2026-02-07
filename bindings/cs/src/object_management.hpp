#pragma once
#include <cstdint>
#include <span>

// Opaque handle for an instance of a custom object
using ObjectHandle = intptr_t;

// Struct size and layout needs to match with the C# side
struct ObjectManagerInterface {
    void (*destroy_object)(ObjectHandle handle);
    void (*free_allocation)(void *allocation);
};

struct ManagedObject {
    explicit ManagedObject(ObjectHandle handle);
    ~ManagedObject();

    [[nodiscard]] ObjectHandle handle() const { return handle_; }
private:
    ObjectHandle handle_;
};

struct ManagedAllocation {
    explicit ManagedAllocation(uint8_t* allocation, size_t size);
    ~ManagedAllocation();

    [[nodiscard]] std::span<const uint8_t> span() const { return {allocation_, size_}; }
private:
    uint8_t *allocation_;
    size_t size_;
};

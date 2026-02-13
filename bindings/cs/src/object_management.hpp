#pragma once

#include "interop.hpp"

struct ManagedObject {
    explicit ManagedObject(const ObjectHandle handle) : handle_(handle) {}
    ~ManagedObject() { interop::free_object(handle_); }

    [[nodiscard]] ObjectHandle handle() const { return handle_; }
private:
    ObjectHandle handle_;
};

#include <cstdint>
#include <new>

#include "export.hpp"

CSHARP_API void *luxon_csharp_malloc(const size_t size) noexcept { return malloc(size); }

CSHARP_API void luxon_csharp_free(void *ptr) noexcept { free(ptr); }

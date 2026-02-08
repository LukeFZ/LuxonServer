#include <cstdint>

#include "export.hpp"
#include <luxon/server/server_manager.hpp>

struct ServerContext {
    server::ServerManager manager;
};

using ServerContextHandle = intptr_t;

namespace {
std::unordered_map<ServerContextHandle, std::unique_ptr<ServerContext>> server_contexts;
std::atomic<ServerContextHandle> next_handle{1};
}

CSHARP_API ServerContextHandle luxon_csharp_server_context_create() {
    const auto handle = next_handle.fetch_add(1);
    server_contexts.emplace(handle, std::make_unique<ServerContext>());
    return handle;
}

CSHARP_API void luxon_csharp_server_context_setup(const ServerContextHandle handle) {
    const auto it = server_contexts.find(handle);
    if (it == server_contexts.end())
        return;
    
    it->second->manager.setup();
}

CSHARP_API void luxon_csharp_server_context_run(const ServerContextHandle handle) {
    const auto it = server_contexts.find(handle);
    if (it == server_contexts.end())
        return;
    
    it->second->manager.run();
}

CSHARP_API void luxon_csharp_server_context_stop(const ServerContextHandle handle) {
    const auto it = server_contexts.find(handle);
    if (it == server_contexts.end())
        return;
    
    it->second->manager.stop();
}

CSHARP_API void luxon_csharp_server_context_configure_server(const ServerContextHandle handle, const char *name, const char *address, const uint16_t port,
                                                             const bool external) {
    const auto it = server_contexts.find(handle);
    if (it == server_contexts.end())
        return;
    
    it->second->manager.configure_server(name, address, port, external);
}

CSHARP_API void luxon_csharp_server_context_destroy(const ServerContextHandle handle) { server_contexts.erase(handle); }
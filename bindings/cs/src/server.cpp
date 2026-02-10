#include <cstdint>
#include <luxon/server/server_manager.hpp>

#include "export.hpp"
#include "handler.hpp"
#include "threading.hpp"

using ServerContextHandle = intptr_t;
using CreateHandlerCallback = ObjectHandle(*)(const char* name, PeerHandle peer);

namespace {
std::unordered_map<ServerContextHandle, std::unique_ptr<server::ServerManager>> server_contexts;
std::atomic<ServerContextHandle> next_handle{1};
CreateHandlerCallback create_handler_callback;
}

CSHARP_API ServerContextHandle luxon_csharp_server_context_create() {
    const auto handle = next_handle.fetch_add(1);
    server_contexts.emplace(handle, std::make_unique<server::ServerManager>());
    return handle;
}

CSHARP_API void luxon_csharp_server_context_setup(const ServerContextHandle handle) { server_contexts[handle]->setup(); }

CSHARP_API void luxon_csharp_server_context_run(const ServerContextHandle handle) { server_contexts[handle]->run(); }

CSHARP_API void luxon_csharp_server_context_stop(const ServerContextHandle handle) { server_contexts[handle]->stop(); }

CSHARP_API void luxon_csharp_server_context_configure_server(const ServerContextHandle handle, const char *name, const char *address, const uint16_t port,
                                                             const bool external) {
    server_contexts[handle]->configure_server(name, address, port, external);
}

CSHARP_API void luxon_csharp_server_context_destroy(const ServerContextHandle handle) { server_contexts.erase(handle); }

CSHARP_API void luxon_csharp_set_create_handler_callback(const CreateHandlerCallback callback) { create_handler_callback = callback; }

// TODO: Maybe move this into handler.cpp and the handler interface
CSHARP_API void luxon_csharp_server_context_register_server(const ServerContextHandle handle, const char* name) {
    const auto str = std::string(name);

    server_contexts[handle]->register_server(str, [str](server::ServerManager& manager, const std::shared_ptr<server::Peer>& peer) {
        ObjectHandle managedObjectHandle;
        const auto peer_handle = register_peer(peer);

        ensure_non_coroutine_call(manager, [&managedObjectHandle, &str, &peer_handle] { 
            managedObjectHandle = create_handler_callback(str.c_str(), peer_handle);
        });

        return std::static_pointer_cast<server::HandlerBase>(
            std::make_shared<CSharpHandler>(manager, peer, std::make_shared<ManagedObject>(managedObjectHandle), peer_handle));
    });
}

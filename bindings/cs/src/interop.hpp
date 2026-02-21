#pragma once

#include <luxon/server/game_plugin_base.hpp>
#include <luxon/enet_protocol.hpp>
#include "export.hpp"

using ObjectHandle = intptr_t;
using PeerHandle = intptr_t;

struct NativeOperationRequestMessage {
    uint8_t operation_code;
    const uint8_t *serialized_parameters;
    size_t serialized_parameters_length;
};

struct NativeOnCreateGameCallInfo {
    bool is_join;
    bool create_if_not_exist;
};

enum class HandlerResult : uint8_t { Return = 0, CallBase = 1 };

struct InteropInterface {
    void(CSHARP_FNPTR free_object)(ObjectHandle handle);

    ObjectHandle (CSHARP_FNPTR create_plugin_instance)(const char *name);
    void (CSHARP_FNPTR destroy_plugin_instance)(ObjectHandle handle);

    server::game_plugins::Result (CSHARP_FNPTR on_attach)(ObjectHandle handle);
    server::game_plugins::Result (CSHARP_FNPTR on_create_game)(ObjectHandle handle, NativeOperationRequestMessage *message, NativeOnCreateGameCallInfo *info);
    server::game_plugins::Result (CSHARP_FNPTR before_join)(ObjectHandle handle);
    server::game_plugins::Result (CSHARP_FNPTR on_join_game)(ObjectHandle handle);

    ObjectHandle(CSHARP_FNPTR create_handler_instance)(const char *name, PeerHandle);
    void (CSHARP_FNPTR destroy_handler_instance)(ObjectHandle handle);

    HandlerResult (CSHARP_FNPTR handle_connect)(ObjectHandle handle);
    HandlerResult (CSHARP_FNPTR handle_disconnect)(ObjectHandle handle);
    HandlerResult (CSHARP_FNPTR handle_update)(ObjectHandle handle);
    HandlerResult (CSHARP_FNPTR handle_slow_update)(ObjectHandle handle);
    HandlerResult (CSHARP_FNPTR handle_operation_request)(ObjectHandle handle, NativeOperationRequestMessage *message, bool encrypted,
                                               const luxon::enet::EnetCommandHeader *header);
};

namespace interop {
void free_object(ObjectHandle handle);

ObjectHandle create_plugin_instance(const std::string& name);
void destroy_plugin_instance(ObjectHandle handle);

server::game_plugins::Result on_attach(ObjectHandle handle);
server::game_plugins::Result on_create_game(ObjectHandle handle, NativeOperationRequestMessage *message, NativeOnCreateGameCallInfo *info);
server::game_plugins::Result before_join(ObjectHandle handle);
server::game_plugins::Result on_join_game(ObjectHandle handle);

ObjectHandle create_handler_instance(const std::string& name, PeerHandle peer_handle);
void destroy_handler_instance(ObjectHandle handle);

HandlerResult handle_connect(ObjectHandle handle);
HandlerResult handle_disconnect(ObjectHandle handle);
HandlerResult handle_update(ObjectHandle handle);
HandlerResult handle_slow_update(ObjectHandle handle);
HandlerResult handle_operation_request(ObjectHandle handle, NativeOperationRequestMessage *message, bool encrypted,
                                       const luxon::enet::EnetCommandHeader *header);
}

#pragma once

#include <luxon/server/handler_base.hpp>

#include "object_management.hpp"
#include "plugin.hpp"

enum class FunctionResult : uint8_t {
    Return = 0,
    CallBase = 1
};

// Struct size and layout needs to match with the C# side
struct ServerHandlerInterface {
    static constexpr auto kFunctionPointerCount = 11;

    void (*destroy_handler_instance)(ObjectHandle handle);
    FunctionResult (*handle_connect)(ObjectHandle handle);
    FunctionResult (*handle_disconnect)(ObjectHandle handle);
    FunctionResult (*handle_update)(ObjectHandle handle);
    FunctionResult (*handle_slow_update)(ObjectHandle handle);
    void *reserved0; // ENetConnectionStateChange
    void *reserved1; // ENetCommand
    void *reserved2; // HTTPRequest
    void *reserved3; // InitRequest
    FunctionResult (*handle_operation_request)(ObjectHandle handle, NativeOperationRequestMessage *message, bool is_encrypted,
                                               const luxon::enet::EnetCommandHeader *header); // OperationRequest
    void *reserved5; // InternalOperationRequest
};
static_assert(sizeof(ServerHandlerInterface) == sizeof(uintptr_t) * ServerHandlerInterface::kFunctionPointerCount);

class CSharpHandler : public server::HandlerBase {
    std::shared_ptr<ManagedObject> object_;

public:
    CSharpHandler(server::ServerManager& manager, const std::shared_ptr<server::Peer>& peer, std::shared_ptr<ManagedObject> object);
    ~CSharpHandler() override;
    void HandleConnect() override;
    void HandleDisconnect() override;
    void HandleUpdate() override;
    void HandleSlowUpdate() override;
    void HandleENetConnectionStateChange(luxon::enet::EnetConnectionState state) override;
    void HandleENetCommand(const luxon::enet::EnetCommand &cmd) override;
    void HandleHTTPRequest(const luxon::HttpRequest &request, const luxon::enet::EnetCommandHeader &cmd_header) override;
    void HandleInitRequest(luxon::ser::InitMessage &req, const luxon::enet::EnetCommandHeader &cmd_header) override;
    void HandleOperationRequest(luxon::ser::OperationRequestMessage &req, bool is_encrypted, const luxon::enet::EnetCommandHeader &cmd_header) override;
    void HandleInternalOperationRequest(luxon::ser::InternalOperationRequestMessage &req, bool is_encrypted,
        const luxon::enet::EnetCommandHeader &cmd_header) override;
};

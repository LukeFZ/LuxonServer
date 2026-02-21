#pragma once

#include <luxon/server/handler_base.hpp>

#include "interop.hpp"
#include "object_management.hpp"

class CSharpHandler : public server::HandlerBase {
    std::shared_ptr<ManagedObject> object_;
    PeerHandle peer_handle_;

public:
    CSharpHandler(server::ServerManager& manager, const std::shared_ptr<server::Peer>& peer, std::shared_ptr<ManagedObject> object, const PeerHandle& handle);
    ~CSharpHandler() override;
    void HandleConnect() override;
    void HandleDisconnect() override;
    void HandleUpdate() override;
    void HandleSlowUpdate() override;
    void HandleENetConnectionStateChange(luxon::enet::EnetConnectionState state) override;
    void HandleENetCommand(const luxon::enet::EnetCommand &cmd) override;
    void HandleHTTPRequest(const luxon::HttpRequest &request, const luxon::enet::EnetCommandHeader &cmd_header) override;
    void HandleInitRequest(const luxon::ser::InitMessage& req, const luxon::enet::EnetCommandHeader& cmd_header) override;
    void HandleOperationRequest(const luxon::ser::OperationRequestMessage& req, bool is_encrypted, const luxon::enet::EnetCommandHeader& cmd_header) override;
    void HandleInternalOperationRequest(const luxon::ser::InternalOperationRequestMessage& req, bool is_encrypted,
        const luxon::enet::EnetCommandHeader &cmd_header) override;
};

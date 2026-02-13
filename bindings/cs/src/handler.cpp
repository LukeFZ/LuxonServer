#include "handler.hpp"
#include "peer.hpp"
#include "threading.hpp"
#include "variant_serialization.hpp"

CSharpHandler::CSharpHandler(server::ServerManager& manager, const std::shared_ptr<server::Peer>& peer, std::shared_ptr<ManagedObject> object, const PeerHandle& handle)
    : HandlerBase(manager, peer), object_(std::move(object)), peer_handle_(handle) {
}

CSharpHandler::~CSharpHandler() {
    ensure_non_coroutine_call(server_manager_, [this] { interop::destroy_handler_instance(object_->handle()); });
    unregister_peer(peer_handle_);
}

void CSharpHandler::HandleConnect() {
    auto result = HandlerResult::Return;
    ensure_non_coroutine_call(server_manager_, [this, &result] { result = interop::handle_connect(object_->handle()); });

    if (result == HandlerResult::CallBase) {
        return HandlerBase::HandleConnect();
    }
}

void CSharpHandler::HandleDisconnect() {
    auto result = HandlerResult::Return;
    ensure_non_coroutine_call(server_manager_, [this, &result] { result = interop::handle_disconnect(object_->handle()); });

    if (result == HandlerResult::CallBase) {
        return HandlerBase::HandleDisconnect();
    }
}

void CSharpHandler::HandleUpdate() {
    auto result = HandlerResult::Return;
    ensure_non_coroutine_call(server_manager_, [this, &result] { result = interop::handle_update(object_->handle()); });

    if (result == HandlerResult::CallBase) {
        return HandlerBase::HandleUpdate();
    }
}

void CSharpHandler::HandleSlowUpdate() {
    auto result = HandlerResult::Return;
    ensure_non_coroutine_call(server_manager_, [this, &result] { result = interop::handle_slow_update(object_->handle()); });

    if (result == HandlerResult::CallBase) {
        return HandlerBase::HandleSlowUpdate();
    }
}

void CSharpHandler::HandleENetConnectionStateChange(luxon::enet::EnetConnectionState state) {
    HandlerBase::HandleENetConnectionStateChange(state);
}

void CSharpHandler::HandleENetCommand(const luxon::enet::EnetCommand &cmd) {
    HandlerBase::HandleENetCommand(cmd);
}

void CSharpHandler::HandleHTTPRequest(const luxon::HttpRequest &request, const luxon::enet::EnetCommandHeader &cmd_header) {
    HandlerBase::HandleHTTPRequest(request, cmd_header);
}

void CSharpHandler::HandleInitRequest(luxon::ser::InitMessage &req, const luxon::enet::EnetCommandHeader &cmd_header) {
    HandlerBase::HandleInitRequest(req, cmd_header);
}

void CSharpHandler::HandleOperationRequest(luxon::ser::OperationRequestMessage &req, bool is_encrypted, const luxon::enet::EnetCommandHeader &cmd_header) {
    auto result = HandlerResult::Return;
    ensure_non_coroutine_call(server_manager_, [&] {
        const auto serialized_parameters = serialize_variant(req.parameters);

        NativeOperationRequestMessage message;
        message.operation_code = req.operation_code;
        message.serialized_parameters = serialized_parameters.data();
        message.serialized_parameters_length = serialized_parameters.size();
        
        result = interop::handle_operation_request(object_->handle(), &message, is_encrypted, &cmd_header);
    });

    if (result == HandlerResult::CallBase) {
        return HandlerBase::HandleOperationRequest(req, is_encrypted, cmd_header);
    }
}

void CSharpHandler::HandleInternalOperationRequest(luxon::ser::InternalOperationRequestMessage &req, bool is_encrypted,
    const luxon::enet::EnetCommandHeader &cmd_header) {
    HandlerBase::HandleInternalOperationRequest(req, is_encrypted, cmd_header);
}

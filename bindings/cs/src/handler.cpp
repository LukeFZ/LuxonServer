#include "handler.hpp"
#include "export.hpp"
#include "threading.hpp"

#ifdef interface
#undef interface
#endif

namespace {
ServerHandlerInterface server_handler_interface;
}

CSHARP_API void luxon_csharp_set_server_handler(const ServerHandlerInterface *interface) { server_handler_interface = *interface; }

CSharpHandler::CSharpHandler(server::ServerManager& manager, const std::shared_ptr<server::Peer> &peer, std::shared_ptr<ManagedObject> object)
    : HandlerBase(manager, peer), object_(std::move(object)) {
}

void CSharpHandler::HandleConnect() {
    auto result = FunctionResult::Return;
    ensure_non_coroutine_call(server_manager_, [this, &result] { result = server_handler_interface.handle_connect(object_->handle()); });

    if (result == FunctionResult::CallBase) {
        return HandlerBase::HandleConnect();
    }
}

void CSharpHandler::HandleDisconnect() {
    auto result = FunctionResult::Return;
    ensure_non_coroutine_call(server_manager_, [this, &result] { result = server_handler_interface.handle_disconnect(object_->handle()); });

    if (result == FunctionResult::CallBase) {
        return HandlerBase::HandleDisconnect();
    }
}

void CSharpHandler::HandleUpdate() {
    auto result = FunctionResult::Return;
    ensure_non_coroutine_call(server_manager_, [this, &result] { result = server_handler_interface.handle_update(object_->handle()); });

    if (result == FunctionResult::CallBase) {
        return HandlerBase::HandleUpdate();
    }
}

void CSharpHandler::HandleSlowUpdate() {
    auto result = FunctionResult::Return;
    ensure_non_coroutine_call(server_manager_, [this, &result] { result = server_handler_interface.handle_slow_update(object_->handle()); });

    if (result == FunctionResult::CallBase) {
        return HandlerBase::HandleSlowUpdate();
    }
}

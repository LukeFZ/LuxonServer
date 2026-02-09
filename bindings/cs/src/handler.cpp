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
    ensure_non_coroutine_call(server_manager_, [this] { server_handler_interface.handle_connect(object_->handle()); });
}

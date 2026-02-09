#include "handler.hpp"

CSharpHandler::CSharpHandler(server::ServerManager& manager, const std::shared_ptr<server::Peer> &peer, std::shared_ptr<ManagedObject> object)
    : HandlerBase(manager, peer), object_(std::move(object)) {
}

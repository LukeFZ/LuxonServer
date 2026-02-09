#pragma once

#include <luxon/server/handler_base.hpp>

#include "object_management.hpp"

class CSharpHandler : public server::HandlerBase {
    std::shared_ptr<ManagedObject> object_;

public:
    CSharpHandler(server::ServerManager& manager, const std::shared_ptr<server::Peer>& peer, std::shared_ptr<ManagedObject> object);
};

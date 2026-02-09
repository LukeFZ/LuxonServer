#pragma once

#include <luxon/server/handler_base.hpp>

#include "object_management.hpp"

enum class FunctionResult : uint8_t {
    Return = 0,
    CallBase = 1
};

// Struct size and layout needs to match with the C# side
struct ServerHandlerInterface {
    static constexpr auto kFunctionPointerCount = 1;
    FunctionResult (*handle_connect)(ObjectHandle handle);
};
static_assert(sizeof(ServerHandlerInterface) == sizeof(uintptr_t) * ServerHandlerInterface::kFunctionPointerCount);

class CSharpHandler : public server::HandlerBase {
    std::shared_ptr<ManagedObject> object_;

public:
    CSharpHandler(server::ServerManager& manager, const std::shared_ptr<server::Peer>& peer, std::shared_ptr<ManagedObject> object);
    void HandleConnect() override;
};

#pragma once
#include <functional>

namespace server {
class ServerManager;
}

void ensure_non_coroutine_call(server::ServerManager& manager, std::move_only_function<void()>&& fn);

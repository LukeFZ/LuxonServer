#include "threading.hpp"

#include <luxon/server/coroutine.hpp>
#include <luxon/server/server_manager.hpp>
#include <luxon/server/sidethread.hpp>

namespace {
server::SideThreadPtr side_thread = server::SideThread::create();
}

void ensure_non_coroutine_call(server::ServerManager& manager, std::move_only_function<void()>&& fn) {
if (minicoro::Coroutine::current() == nullptr) {
        // We're in a side thread or the main thread, just call the function directly
        return fn();
    }

    // We're in a coroutine, so we need to switch to the side thread
    manager.call_in_side_thread(side_thread, std::move(fn));
}

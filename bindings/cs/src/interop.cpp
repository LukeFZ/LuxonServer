#include "interop.hpp"

#include "export.hpp"

namespace {
InteropInterface interop_interface;
}

CSHARP_API void luxon_csharp_set_interop_interface(const InteropInterface* interface) { interop_interface = *interface; }

void interop::free_object(ObjectHandle handle) {

}

ObjectHandle interop::create_plugin_instance(const std::string &name) { return interop_interface.create_plugin_instance(name.c_str()); }

void interop::destroy_plugin_instance(ObjectHandle handle) { return interop_interface.destroy_plugin_instance(handle); }

server::game_plugins::Result interop::on_attach(ObjectHandle handle) { return interop_interface.on_attach(handle); }

server::game_plugins::Result interop::on_create_game(ObjectHandle handle, NativeOperationRequestMessage *message, NativeOnCreateGameCallInfo *info) {
    return interop_interface.on_create_game(handle, message, info);
}

server::game_plugins::Result interop::before_join(ObjectHandle handle) { return interop_interface.before_join(handle); }

server::game_plugins::Result interop::on_join_game(ObjectHandle handle) { return interop_interface.on_join_game(handle); }

ObjectHandle interop::create_handler_instance(const std::string &name, PeerHandle peer_handle) {
    return interop_interface.create_handler_instance(name.c_str(), peer_handle);
}

void interop::destroy_handler_instance(ObjectHandle handle) { return interop_interface.destroy_handler_instance(handle); }

HandlerResult interop::handle_connect(ObjectHandle handle) { return interop_interface.handle_connect(handle); }

HandlerResult interop::handle_disconnect(ObjectHandle handle) { return interop_interface.handle_disconnect(handle); }

HandlerResult interop::handle_update(ObjectHandle handle) { return interop_interface.handle_update(handle); }

HandlerResult interop::handle_slow_update(ObjectHandle handle) { return interop_interface.handle_slow_update(handle); }

HandlerResult interop::handle_operation_request(ObjectHandle handle, NativeOperationRequestMessage *message, bool encrypted,
    const luxon::enet::EnetCommandHeader *header) {
    return interop_interface.handle_operation_request(handle, message, encrypted, header);
}

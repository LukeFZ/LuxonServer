#include "plugin.hpp"

#include "export.hpp"
#include "luxon/server/game_plugin_registry.hpp"

namespace {
PluginManagerInterface plugin_manager_interface;
}

CSHARP_API void luxon_csharp_set_plugin_manager(const PluginManagerInterface *interface) { plugin_manager_interface = *interface; }

CSHARP_API void luxon_csharp_register_plugin(const char* name) {
    const auto name_str = std::string(name);
    
    server::game_plugins::registry::register_(name_str, [&name_str](server::Game *game) { 
        const auto handle = plugin_manager_interface.create_plugin_instance(name_str.c_str());
        return std::make_unique<CSharpPlugin>(game, name_str, std::make_shared<ManagedObject>(handle));
    });
}

CSharpPlugin::CSharpPlugin(server::Game *game, const std::string_view plugin_name, std::shared_ptr<ManagedObject> object) 
    : PluginBase(game, plugin_name), object_(std::move(object)) { }

CSharpPlugin::~CSharpPlugin() { plugin_manager_interface.destroy_plugin_instance(object_->handle()); }

void CSharpPlugin::OnAttach() { plugin_manager_interface.on_attach(object_->handle()); }

server::game_plugins::Result CSharpPlugin::OnCreateGame(luxon::ser::OperationRequestMessage &req,
    server::game_plugins::OnCreateGameCallInfo &onCreateGameCallInfo) {
    return plugin_manager_interface.on_create_game(object_->handle());
}

server::game_plugins::Result CSharpPlugin::BeforeJoin(luxon::ser::OperationRequestMessage &req,
    server::game_plugins::BeforeJoinGameCallInfo &beforeJoinGameCallInfo) {
    return PluginBase::BeforeJoin(req, beforeJoinGameCallInfo);
}

server::game_plugins::Result CSharpPlugin::OnJoinGame(luxon::ser::OperationRequestMessage &req, server::game_plugins::OnJoinGameCallInfo &onJoinGameCallInfo) {
    return PluginBase::OnJoinGame(req, onJoinGameCallInfo);
}

server::game_plugins::Result CSharpPlugin::OnLeave(luxon::ser::OperationRequestMessage &req, server::game_plugins::OnLeaveGameCallInfo &onLeaveGameCallInfo) {
    return PluginBase::OnLeave(req, onLeaveGameCallInfo);
}

server::game_plugins::Result CSharpPlugin::OnRaiseEvent(luxon::ser::OperationRequestMessage &req,
    server::game_plugins::OnRaiseEventCallInfo &onRaiseEventCallInfo) {
    return PluginBase::OnRaiseEvent(req, onRaiseEventCallInfo);
}

server::game_plugins::Result CSharpPlugin::BeforeSetProperties(luxon::ser::OperationRequestMessage &req,
    server::game_plugins::BeforeSetPropertiesCallInfo &beforeSetPropertiesCallInfo) {
    return PluginBase::BeforeSetProperties(req, beforeSetPropertiesCallInfo);
}

server::game_plugins::Result CSharpPlugin::OnSetProperties(luxon::ser::OperationRequestMessage &req,
    server::game_plugins::OnSetPropertiesCallInfo &onSetPropertiesCallInfo) {
    return PluginBase::OnSetProperties(req, onSetPropertiesCallInfo);
}

server::game_plugins::Result CSharpPlugin::BeforeCloseGame(server::game_plugins::BeforeCloseGameCallInfo &beforeCloseGameCallInfo) {
    return PluginBase::BeforeCloseGame(beforeCloseGameCallInfo);
}

server::game_plugins::Result CSharpPlugin::OnCloseGame(server::game_plugins::OnCloseGameCallInfo &onCloseGameCallInfo) {
    return PluginBase::OnCloseGame(onCloseGameCallInfo);
}

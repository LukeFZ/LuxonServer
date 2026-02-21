#include "plugin.hpp"

#include "export.hpp"
#include "threading.hpp"

#include <luxon/server/game.hpp>
#include <luxon/server/game_plugin_registry.hpp>

#include "variant_serialization.hpp"

namespace {
server::logger logger("CSharpPlugin");
}

CSHARP_API void luxon_csharp_register_plugin(const char* name) {
    const auto name_str = std::string(name);
    logger.info("Registering C# plugin: {}", name_str);
    
    server::game_plugins::registry::register_(name_str, [name_str](server::Game *game) {
        ObjectHandle handle = 0;

        ensure_non_coroutine_call(game->lobby->app->server_manager,
                                  [&handle, &name_str] { handle = interop::create_plugin_instance(name_str); });

        return std::make_unique<CSharpPlugin>(game, name_str, std::make_shared<ManagedObject>(handle));
    });
}

CSharpPlugin::CSharpPlugin(server::Game *game, const std::string_view plugin_name, std::shared_ptr<ManagedObject> object) 
    : PluginBase(game, plugin_name), object_(std::move(object)) { }

CSharpPlugin::~CSharpPlugin() {
    ensure_non_coroutine_call(game_->lobby->app->server_manager, [this] { interop::destroy_plugin_instance(object_->handle()); });
}

void CSharpPlugin::OnAttach() { 
    ensure_non_coroutine_call(game_->lobby->app->server_manager, [this] { interop::on_attach(object_->handle()); });
}

server::game_plugins::Result CSharpPlugin::OnCreateGame(const luxon::ser::OperationRequestMessage& req,
    server::game_plugins::OnCreateGameCallInfo &onCreateGameCallInfo) {
    server::game_plugins::Result result{};
    ensure_non_coroutine_call(game_->lobby->app->server_manager, [&] {
        const auto serialized_parameters = serialize_variant(req.parameters);

        NativeOperationRequestMessage message;
        message.operation_code = req.operation_code;
        message.serialized_parameters = serialized_parameters.data();
        message.serialized_parameters_length = serialized_parameters.size();

        NativeOnCreateGameCallInfo info{.is_join = onCreateGameCallInfo.is_join, .create_if_not_exist = onCreateGameCallInfo.create_if_not_exist};

        result = interop::on_create_game(object_->handle(), &message, &info);
    });
    return result;
}

server::game_plugins::Result CSharpPlugin::BeforeJoin(const luxon::ser::OperationRequestMessage& req,
    server::game_plugins::BeforeJoinGameCallInfo &beforeJoinGameCallInfo) {
    server::game_plugins::Result result{};
    ensure_non_coroutine_call(game_->lobby->app->server_manager, [&result, this] { result = interop::before_join(object_->handle()); });
    return result;
}

server::game_plugins::Result CSharpPlugin::OnJoinGame(const luxon::ser::OperationRequestMessage& req,
                                                      server::game_plugins::OnJoinGameCallInfo& onJoinGameCallInfo) {
    server::game_plugins::Result result{};
    ensure_non_coroutine_call(game_->lobby->app->server_manager, [&result, this] { result = interop::on_join_game(object_->handle()); });
    return result;
}

server::game_plugins::Result CSharpPlugin::OnLeave(const luxon::ser::OperationRequestMessage& req,
                                                   server::game_plugins::OnLeaveGameCallInfo& onLeaveGameCallInfo) {
    return PluginBase::OnLeave(req, onLeaveGameCallInfo);
}

server::game_plugins::Result CSharpPlugin::OnRaiseEvent(const luxon::ser::OperationRequestMessage& req,
    server::game_plugins::OnRaiseEventCallInfo &onRaiseEventCallInfo) {
    return PluginBase::OnRaiseEvent(req, onRaiseEventCallInfo);
}

server::game_plugins::Result CSharpPlugin::BeforeSetProperties(const luxon::ser::OperationRequestMessage& req,
    server::game_plugins::BeforeSetPropertiesCallInfo &beforeSetPropertiesCallInfo) {
    return PluginBase::BeforeSetProperties(req, beforeSetPropertiesCallInfo);
}

server::game_plugins::Result CSharpPlugin::OnSetProperties(const luxon::ser::OperationRequestMessage& req,
    server::game_plugins::OnSetPropertiesCallInfo &onSetPropertiesCallInfo) {
    return PluginBase::OnSetProperties(req, onSetPropertiesCallInfo);
}

server::game_plugins::Result CSharpPlugin::BeforeCloseGame(server::game_plugins::BeforeCloseGameCallInfo &beforeCloseGameCallInfo) {
    return PluginBase::BeforeCloseGame(beforeCloseGameCallInfo);
}

server::game_plugins::Result CSharpPlugin::OnCloseGame(server::game_plugins::OnCloseGameCallInfo &onCloseGameCallInfo) {
    return PluginBase::OnCloseGame(onCloseGameCallInfo);
}

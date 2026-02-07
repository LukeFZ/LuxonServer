#pragma once

#include <luxon/server/game_plugin_base.hpp>

using PluginInstanceHandle = uint64_t;

// Struct size and layout needs to match with the C# side
struct PluginManagerInterface {
    PluginInstanceHandle (*create_plugin_instance)(const char *plugin_name);
    void (*destroy_plugin_instance)(PluginInstanceHandle handle);
    server::game_plugins::Result (*on_attach)(PluginInstanceHandle);
    server::game_plugins::Result (*on_create_game)(PluginInstanceHandle);
};
static_assert(sizeof(PluginManagerInterface) == sizeof(uintptr_t) * 4);

class CSharpPlugin : public server::game_plugins::PluginBase {
public:
    CSharpPlugin(server::Game *game, std::string_view plugin_name, PluginInstanceHandle handle);
    ~CSharpPlugin() override;
    void OnAttach() override;
    server::game_plugins::Result OnCreateGame(luxon::ser::OperationRequestMessage& req, server::game_plugins::OnCreateGameCallInfo&) override;
    server::game_plugins::Result BeforeJoin(luxon::ser::OperationRequestMessage& req, server::game_plugins::BeforeJoinGameCallInfo&) override;
    server::game_plugins::Result OnJoinGame(luxon::ser::OperationRequestMessage& req, server::game_plugins::OnJoinGameCallInfo&) override;
    server::game_plugins::Result OnLeave(luxon::ser::OperationRequestMessage& req, server::game_plugins::OnLeaveGameCallInfo&) override;
    server::game_plugins::Result OnRaiseEvent(luxon::ser::OperationRequestMessage& req, server::game_plugins::OnRaiseEventCallInfo&) override;
    server::game_plugins::Result BeforeSetProperties(luxon::ser::OperationRequestMessage& req, server::game_plugins::BeforeSetPropertiesCallInfo&) override;
    server::game_plugins::Result OnSetProperties(luxon::ser::OperationRequestMessage& req, server::game_plugins::OnSetPropertiesCallInfo&) override;
    server::game_plugins::Result BeforeCloseGame(server::game_plugins::BeforeCloseGameCallInfo&) override;
    server::game_plugins::Result OnCloseGame(server::game_plugins::OnCloseGameCallInfo&) override;
private:
    PluginInstanceHandle handle_;
};
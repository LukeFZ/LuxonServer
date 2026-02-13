#pragma once

#include <luxon/server/game_plugin_base.hpp>

#include "object_management.hpp"

class CSharpPlugin : public server::game_plugins::PluginBase {
public:
    CSharpPlugin(server::Game *game, std::string_view plugin_name, std::shared_ptr<ManagedObject> object);
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
    std::shared_ptr<ManagedObject> object_;
};
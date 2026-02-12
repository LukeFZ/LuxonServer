#include "export.hpp"
#include <algorithm>
#include <luxon/server/game.hpp>

#include "variant_serialization.hpp"

CSHARP_API bool luxon_csharp_game_peer_has_interest_group(const server::GamePeer* game_peer, const uint8_t group) {
    if (!game_peer)
	return false;

    return game_peer->has_interest_group(group);
}

CSHARP_API bool luxon_csharp_game_peer_disconnect(server::GamePeer* game_peer) {
    if (!game_peer)
        return false;

    return game_peer->disconnect();
}

CSHARP_API int32_t luxon_csharp_game_peer_get_actor_id(const server::GamePeer* game_peer) {
    if (!game_peer)
        return 0;

    return game_peer->actor_id;
}

CSHARP_API size_t luxon_csharp_game_peer_get_actor_props(const server::GamePeer* game_peer, uint8_t* out_serialized) {
    if (!game_peer)
        return 0;

    const auto serialized = serialize_variant(game_peer->actor_props);
    if (out_serialized)
        std::ranges::copy(serialized, out_serialized);

    return serialized.size();
}

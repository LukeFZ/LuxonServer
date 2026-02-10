#include "peer.hpp"

#include <unordered_map>
#include <luxon/server/peer.hpp>
#include <luxon/ser_types.hpp>

#include "export.hpp"
#include "variant_serialization.hpp"

namespace {
std::unordered_map<PeerHandle, std::weak_ptr<server::Peer>> registered_peers;
std::atomic<PeerHandle> next_handle{1};
}

CSHARP_API void luxon_csharp_peer_send_message(PeerHandle handle, const SendMessageArguments* arguments) {
    if (const auto peer = registered_peers[handle].lock()) {
        switch (arguments->type) {
        case MessageType::RawData: {
            const auto raw = arguments->raw;
            const auto byte_array = luxon::ser::ByteArray(raw->data, raw->data + raw->size);
            peer->send(byte_array, arguments->options);
            break;
        }
        case MessageType::Event: {
            const auto event = arguments->event;
            const auto is_encrypted = !!(arguments->flags & MessageFlags::IsEncrypted);

            auto serialized_parameters = luxon::ser::ByteArray(event->serialized_parameters, event->serialized_parameters + event->serialized_parameters_size);
            const auto parameters = deserialize_variant(serialized_parameters).get<luxon::ser::ParameterList>();
            auto evt = luxon::ser::EventMessage{.event_code = event->event_code, .parameters = parameters};

            if (const auto serialized = peer->protocol->Serialize(evt, is_encrypted)) {
                peer->send(*serialized, arguments->options);
            }
            break;
        }
        case MessageType::OperationResponse: {
            const auto response = arguments->operation_response;
            const auto is_encrypted = !!(arguments->flags & MessageFlags::IsEncrypted);
            auto serialized_parameters =
                luxon::ser::ByteArray(response->serialized_parameters, response->serialized_parameters + response->serialized_parameters_size);

            const auto parameters = deserialize_variant(serialized_parameters).get<luxon::ser::ParameterList>();
            auto operation_response = luxon::ser::OperationResponseMessage{
                .operation_code = response->operation_code, 
                .return_code = response->return_code,
                .debug_message = std::nullopt,
                .parameters = parameters
            };
            if (response->debug_message != nullptr) {
                operation_response.debug_message = response->debug_message;
            }

            if (const auto serialized = peer->protocol->Serialize(operation_response, is_encrypted)) {
                peer->send(*serialized, arguments->options);
            }
            break;
        }
        }
    }
}

PeerHandle register_peer(const std::shared_ptr<server::Peer>& peer) {
    const auto handle = next_handle.fetch_add(1);
    registered_peers[handle] = peer;
    return handle;
}

void unregister_peer(const PeerHandle &handle) { registered_peers.erase(handle); }

#pragma once

#include <cstdint>
#include <memory>
#include <luxon/enet_peer.hpp>

#include "interop.hpp"

namespace server {
struct Peer;
}

enum class MessageType : uint8_t {
    RawData = 0,
    OperationResponse = 1,
    Event = 2,
};

enum MessageFlags : uint8_t {
    IsEncrypted
};

struct RawMessageData {
    uint8_t *data;
    size_t size;
};

struct EventMessageData {
    uint8_t event_code;
    uint8_t *serialized_parameters;
    size_t serialized_parameters_size;
};

struct OperationResponseMessageData {
    uint8_t operation_code;
    int16_t return_code;
    const char *debug_message;
    uint8_t *serialized_parameters;
    size_t serialized_parameters_size;
};

struct SendMessageArguments {
    luxon::enet::EnetSendOptions options;
    MessageType type;
    std::underlying_type_t<MessageFlags> flags;
    union {
        RawMessageData* raw;
        EventMessageData* event;
        OperationResponseMessageData* operation_response;
    };
};

PeerHandle register_peer(const std::shared_ptr<server::Peer>& peer);
void unregister_peer(const PeerHandle& handle);

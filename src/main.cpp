// Copyright (c) 2026, the Luxon Server contributors
// SPDX-License-Identifier: BSD-3-Clause

#include "luxon/server/server_manager.hpp"

int main() {
    server::ServerManager("config.yml").run();
}

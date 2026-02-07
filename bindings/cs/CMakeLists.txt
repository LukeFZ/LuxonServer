cmake_minimum_required(VERSION 3.21)
project(LuxonCSharpPlugin)

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

if (MSVC)
    add_compile_definitions(NOMINMAX)
endif()

file(GLOB SOURCE_FILES "src/*.cpp" "src/*.hpp")

add_library(luxon_csharp_plugin STATIC ${SOURCE_FILES})

luxon_register_plugin(luxon_csharp_plugin)
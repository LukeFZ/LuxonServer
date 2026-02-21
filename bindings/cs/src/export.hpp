#pragma once

#if defined(_WIN32) | defined(_WIN64)
#define CSHARP_API extern "C" __declspec(dllexport)
#define CSHARP_FNPTR __cdecl*
#else
#define CSHARP_API extern "C" __attribute__((visibility("default")))
#define CSHARP_FNPTR *
#endif

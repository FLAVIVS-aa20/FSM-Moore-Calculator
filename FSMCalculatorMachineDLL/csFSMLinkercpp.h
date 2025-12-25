#pragma once

#ifdef FSMCALCULATORDLL_EXPORTS
#define DLL_API __declspec(dllexport)
#else
#define DLL_API __declspec(dllimport)
#endif

extern "C" {
    struct Context;  // Opaque FSM context for C#

    DLL_API Context* __cdecl CreateContext();
    DLL_API void __cdecl HandleInput(Context* ctx, const char* token);
    DLL_API const char* __cdecl GetDisplay(Context* ctx);
    DLL_API void __cdecl DeleteContext(Context* ctx);
}

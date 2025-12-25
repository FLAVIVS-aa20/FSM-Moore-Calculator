#include "csFSMLinkercpp.h"
#include "Context.h"   // clsContext FSM
#include <memory>
#include <string>

struct Context {
    std::unique_ptr<clsContext> fsm; // FSM object
    std::string displayBuffer;        // buffer for GetDisplay
};

extern "C" {

    DLL_API Context* __cdecl CreateContext() {
        Context* ctx = new Context();
        ctx->fsm = std::make_unique<clsContext>();
        ctx->fsm->Transition(clsStateType::Start);
        ctx->displayBuffer = ctx->fsm->getdisplay();
        return ctx;
    }

    DLL_API void __cdecl HandleInput(Context* ctx, const char* token) {
        if (!ctx || !token) return;

        std::string t(token);
        ctx->fsm->HandleInput(t);                 // FSM handles input
        ctx->displayBuffer = ctx->fsm->getdisplay(); // update buffer
    }

    DLL_API const char* __cdecl GetDisplay(Context* ctx) {
        if (!ctx) return "0";
        return ctx->displayBuffer.c_str(); // stable pointer
    }

    DLL_API void __cdecl DeleteContext(Context* ctx) {
        delete ctx;  // safely deletes FSM via unique_ptr
    }

}

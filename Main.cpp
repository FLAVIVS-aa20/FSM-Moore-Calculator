#include <iostream>
#include "StateType.h"
#include "Context.h"
#include "Start.h"
#include "AC.h"
#include "OpWait.h"
#include "Compute.h"
#include "Error.h"

using namespace std;

void simulateInput(clsContext &ctx, const string &input) {
    for(char c : input) {
        string token(1, c); // convert char to string
        ctx.HandleInput(token);
        cout << "Input: " << c << " | Display: " << ctx.getdisplay() << endl;
    }
}


int main(){
    clsContext ctx;
    
   
    ctx.SetCurrentState(new clsS());  // Start state

    cout << "--- Test 1: Simple Addition -5 + 3 = ---" << endl;
    simulateInput(ctx, "-5");   
    simulateInput(ctx, "+");    
    simulateInput(ctx, "3");    
    simulateInput(ctx, "=");   
    cout << "Result: " << ctx.getdisplay() << endl;

    cout << "\n--- Test 2: Clear ---" << endl;
    simulateInput(ctx, "C");    
    cout << "Display after clear: " << ctx.getdisplay() << endl;

    cout << "\n--- Test 3: Division by zero 5 / 0 = ---" << endl;
    simulateInput(ctx, "5");
    simulateInput(ctx, "/");
    simulateInput(ctx, "0");
    simulateInput(ctx, "=");
    cout << "Display: " << ctx.getdisplay() << endl;

    cout << "\n--- Test 4: Invalid input ---" << endl;
    simulateInput(ctx, "a"); // invalid character
    cout << "Display: " << ctx.getdisplay() << endl;

    cout << "n--- Test 5: computing three numbers ---" << endl;
    simulateInput(ctx, "2");
    simulateInput(ctx, "+");
    simulateInput(ctx, "4");
    simulateInput(ctx, "+");   
    simulateInput(ctx, "6");    
    simulateInput(ctx, "=");   
    cout << "Result: " << ctx.getdisplay() << endl;

    return 0;
}
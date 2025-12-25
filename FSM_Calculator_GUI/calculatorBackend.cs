using System;
using System.Runtime.InteropServices;

namespace FSMCalculator
{
    public class CalculatorBackend : IDisposable
    {
        private IntPtr _ctx;

        [DllImport("FSMCalculatorMachineDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateContext();

        [DllImport("FSMCalculatorMachineDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void HandleInput(IntPtr ctx, string token);

        [DllImport("FSMCalculatorMachineDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetDisplay(IntPtr ctx);

        [DllImport("FSMCalculatorMachineDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void DeleteContext(IntPtr ctx);

        public CalculatorBackend()
        {
            _ctx = CreateContext();
            if (_ctx == IntPtr.Zero)
                throw new Exception("Failed to create FSM context.");
        }

        public void Send(string token)
        {
            if (_ctx == IntPtr.Zero)
                throw new ObjectDisposedException(nameof(CalculatorBackend));

            HandleInput(_ctx, token);
        }

        public string Display
        {
            get
            {
                if (_ctx == IntPtr.Zero) return "0";
                return Marshal.PtrToStringAnsi(GetDisplay(_ctx)) ?? "0";
            }
        }

        public void Dispose()
        {
            if (_ctx != IntPtr.Zero)
            {
                DeleteContext(_ctx);
                _ctx = IntPtr.Zero;
            }
        }
    }
}

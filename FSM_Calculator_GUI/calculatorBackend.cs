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
                throw new Exception("Failed to create backend context.");
        }

        public void HandleInput(string token)
        {
            if (_ctx == IntPtr.Zero) throw new Exception("Backend context not initialized.");
            HandleInput(_ctx, token);
        }

        public string GetDisplay()
        {
            if (_ctx == IntPtr.Zero) return "0";
            IntPtr ptr = GetDisplay(_ctx);
            return Marshal.PtrToStringAnsi(ptr);
        }

        public void Clear()
        {
            HandleInput("C");
        }

        public string CalculateResult()
        {
            HandleInput("=");
            return GetDisplay();
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

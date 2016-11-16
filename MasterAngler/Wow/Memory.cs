using System;
using System.Diagnostics;
using System.Drawing;

namespace MasterAngler.Wow {
    public static class Memory {
        public static uint BaseAddress { get; private set; }

        public static MemoryReader GameMemory { get; private set; }
        public static IntPtr WindowHandle { get; private set; }

        static Memory() {
            GameMemory = new MemoryReader();
        }

        public static Point WindowSize
        {
            get
            {
                NativeMethods.Rect wowWindowRect = ClientRect;
                return new Point(wowWindowRect.Right, wowWindowRect.Bottom);
            }
        }

        public static NativeMethods.Rect ClientRect => NativeMethods.GetClientRect(WindowHandle);

        public static NativeMethods.Rect ClientWindow => NativeMethods.GetWindowRect(WindowHandle);

        public static bool Initialize(int processId) {
            try {
                Process.GetProcessById(processId);
                if (!GameMemory.Open(processId)) {
                    return false;
                }

                BaseAddress = (uint) GameMemory.MainModule.BaseAddress;
                WindowHandle = Process.GetProcessById(processId).MainWindowHandle;

                return true;
            } catch (ArgumentException) {
                return false;
            } catch (Exception) {
                return false;
            }
        }
    }
}
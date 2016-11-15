using System;
using System.Diagnostics;
using System.Drawing;

namespace MasterAngler.Wow {
    public static class Memory {
        public static uint BaseAddress { get; private set; }

        public static MemoryReader GameMemory { get; private set; }

        static Memory() {
            GameMemory = new MemoryReader();
        }

        public static Point WowWindowSize
        {
            get
            {
                NativeMethods.Rect wowWindowRect = WowWindowRect;
                return new Point(wowWindowRect.Right - wowWindowRect.Left, wowWindowRect.Bottom - wowWindowRect.Top);
            }
        }

        public static NativeMethods.Rect WowWindowRect => NativeMethods.GetClientRect(GameMemory.WowWindowHandle);

        public static bool Initialize(int processId) {
            try {
                Process.GetProcessById(processId);
                if (!GameMemory.Open(processId)) {
                    return false;
                }

                BaseAddress = (uint) GameMemory.MainModule.BaseAddress;

                return true;
            } catch (ArgumentException) {
                return false;
            } catch (Exception) {
                return false;
            }
        }
    }
}
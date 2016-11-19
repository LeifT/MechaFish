using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MechaFish.Wow {
    public static class Keyboard {
        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;
        private const uint WM_CHAR = 0x0102;

        public static void KeyDown(Keys key) {
            NativeMethods.SendMessage(Memory.WindowHandle, WM_KEYDOWN, new IntPtr((long)key), (IntPtr) 0);
        }

        public static void KeyUp(Keys key) {
            NativeMethods.SendMessage(Memory.WindowHandle, WM_KEYUP, new IntPtr((long)key), (IntPtr) 0);
        }

        public static void KeyPress(Keys key) {
            KeyDown(key);
            KeyUp(key);
        }
    }
}
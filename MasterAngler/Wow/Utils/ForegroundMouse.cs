using System;
using System.Drawing;
using System.Threading;
using MasterAngler.Wow.ObjectManager;

namespace MasterAngler.Wow.Utils {
    public class ForegroundMouse : IMouseStrategy {
        public bool SetMouseOver(WowObject wowObject) {
            Point point = new Point();

            var isWithinScreen = WowCamera.ActiveCamera.WorldToScreen(wowObject.Position, ref point);

            if (!isWithinScreen) {
                return false;
            }

            //var a = NativeMethods.GetClientRect(Memory.GameMemory.WowWindowHandle);


            //if (point.X < a.Left) {
            //    point.X = a.Left;
            //}

            //if (point.X > a.Right){
            //    point.X = a.Right - 1;
            //}

            //if (point.Y < a.Top) {
            //    point.Y = a.Top;
            //}

            //if (point.Y > a.Bottom) {
            //    point.Y = a.Bottom -1;
            //}

            MoveMouse(point.X, point.Y);
            return wowObject.IsMouseOver;
        }

        private void MoveMouse(int x, int y) {
            Point point = NativeMethods.ClientToScreen(Memory.GameMemory.WowWindowHandle, x, y);
            NativeMethods.SetCursorPos(point.X, point.Y);
            NativeMethods.SendMessage(Memory.GameMemory.WowWindowHandle, 512, IntPtr.Zero, Lpraram(x, y));
        }
        
        private static IntPtr Lpraram(int x, int y) {
            return (IntPtr)(y << 0x10 | x & 0xFFFF);
        }
    }
}
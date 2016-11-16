using System;
using System.Drawing;
using System.Threading;
using MasterAngler.Wow.ObjectManager;

namespace MasterAngler.Wow.Utils {
    public class ForegroundMouse : IMouseStrategy {

        private Point _position;

        private Point Position {
            get {
                _position = NativeMethods.GetCursorPos();
                return _position;
            }

            set {
                NativeMethods.SetCursorPos(value.X, value.Y);
            }
        }

        public bool SetMouseOver(WowObject wowObject) {
            Point objectClientPosition = new Point();
            
            var visible = WowCamera.ActiveCamera.WorldToScreen(wowObject.Position, ref objectClientPosition);

            if (!visible) {
                return false;
            }

            Point start = Position;
            Point target = NativeMethods.ClientToScreen(Memory.WindowHandle, objectClientPosition.X, objectClientPosition.Y);

            Point pos = NativeMethods.ClientToScreen(Memory.WindowHandle, 0, 0);
            Point size = Memory.WindowSize;

            float t = 0;

            while (!wowObject.IsMouseOver) {
                Position = Lerp(start, target, t);

                if (IsMouseInGameWindow(_position, size, pos)) {
                    objectClientPosition = NativeMethods.ScreenToClient(Memory.WindowHandle, _position.X, _position.Y);
                    NativeMethods.SendMessage(Memory.WindowHandle, 512, IntPtr.Zero, Lpraram(objectClientPosition));
                }

                t += 0.001f;
                Thread.Sleep(1);
            }


            //float t = 0;

            //while (!wowObject.IsMouseOver)
            //{
            //    NativeMethods.SetCursorPos(Lerp(start.X, target.X, t), Lerp(start.Y, target.Y, t));

            //    //SetMouse(x, y);

            //    t += 0.001f;
            //    Thread.Sleep(1);
            //}

            //SetMouse(point.X, point.Y);
            return wowObject.IsMouseOver;
        }

        private int Lerp(int start, int target, float t) {
            if (t < 0) {
                t = 0;
            }

            if (t > 1) {
                t = 1;
            }

            return (int) (start*(1.0 - t) + target*t);
        }

        private Point Lerp(Point start, Point end, float t) {
            return new Point(Lerp(start.X, end.X, t), Lerp(start.Y, end.Y, t));
        }

        private bool IsMouseInGameWindow(Point cursorPos, Point size, Point pos) {
            //Point size = Memory.WindowSize;
            //Point pos = NativeMethods.ClientToScreen(Memory.WindowHandle, 0, 0);
            //Point cursorPos = NativeMethods.GetCursorPos();

            if (cursorPos.X < pos.X) {
                return false;
            }

            if (cursorPos.X > size.X + pos.X) {
                return false;
            }

            if (cursorPos.Y < pos.Y) {
                return false;
            }

            if (cursorPos.Y > pos.Y + size.Y) {
                return false;
            }

            return true;
        }


        private void SetMouse(int x, int y)
        {
            //Point objectScreenPosition = NativeMethods.ClientToScreen(Memory.WindowHandle, x, y);
            ////Point cursorPosition = NativeMethods.GetCursorPos();

            //// Game window
            //if (IsMouseInGameWindow())
            //{
            //    NativeMethods.SendMessage(Memory.WindowHandle, 512, IntPtr.Zero, Lpraram(x, y));
            //}

            //return;

            //Screen
           Point mouseScreenPoint = NativeMethods.ClientToScreen(Memory.WindowHandle, x, y);
            NativeMethods.SetCursorPos(mouseScreenPoint.X, mouseScreenPoint.Y);

            ////Game window
            //if (IsMouseInGameWindow())
            //{
            //    NativeMethods.SendMessage(Memory.WindowHandle, 512, IntPtr.Zero, Lpraram(x, y));
            //}
        }

        private static IntPtr Lpraram(Point p) {
            return (IntPtr)(p.Y << 0x10 | p.X & 0xFFFF);
        }
    }
}
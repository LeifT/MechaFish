﻿using System;
using System.Diagnostics;
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
                if (_position.X == value.X && _position.Y == value.Y) {
                    return;
                }

                _position.X = value.X;
                _position.Y = value.Y;
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
            Point end = NativeMethods.ClientToScreen(Memory.WindowHandle, objectClientPosition.X, objectClientPosition.Y);

            Point clientPos = NativeMethods.ClientToScreen(Memory.WindowHandle, 0, 0);
            Point clientSize = Memory.WindowSize;

          

            float t = 0;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            long oldTime = timer.ElapsedMilliseconds;
            Point lastPos = _position;

            while (!wowObject.IsMouseOver) {
                var currentTime = timer.ElapsedMilliseconds;
                float deltatime = ((currentTime - oldTime));
                oldTime = currentTime;

                Position = MoveTowards(start, end, t);

                //if (IsMouseInGameWindow(_position, clientSize, clientPos) && lastPos.X != _position.X && lastPos.Y != _position.Y) {
                //    lastPos = _position;
                //    objectClientPosition = NativeMethods.ScreenToClient(Memory.WindowHandle, _position.X, _position.Y);
                //    NativeMethods.SendMessage(Memory.WindowHandle, 512, IntPtr.Zero, Lpraram(objectClientPosition));
                //}

                t += deltatime;
            }
            return wowObject.IsMouseOver;
        }

        private float Lerp(float start, float target, float t) {
            if (t < 0) {
                t = 0;
            }

            if (t > 1) {
                t = 1;
            }

            return (start*(1.0f - t) + target*t);
        }

        private Point Lerp(Point start, Point end, float t) {
            return new Point((int)Lerp(start.X, end.X, t), (int)Lerp(start.Y, end.Y, t));
        }

        public static Point MoveTowards(Point current, Point target, float maxDistanceDelta)
        {
            Point a = new Point(target.X - current.X, target.Y - current.Y);

            float magnitude = (float) Math.Sqrt(a.X * a.X + a.Y * a.Y);

            if (magnitude <= maxDistanceDelta || Math.Abs(magnitude) < float.Epsilon) {
                return target;
            }

            var b = (a.X/magnitude);
            var c = (a.Y/magnitude);


            return new Point((int)(current.X + b * maxDistanceDelta), (int)(current.Y +  c * maxDistanceDelta));
        }

        public static float MoveTowards(float current, float target, float maxDelta) {
            if (Math.Abs(target - current) <= maxDelta) {
                return target;
            }
            return current + Math.Sign(target - current) * maxDelta;
        }

        private bool IsMouseInGameWindow(Point cursorPos, Point size, Point pos) {
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

        private static IntPtr Lpraram(Point p) {
            return (IntPtr)(p.Y << 0x10 | p.X & 0xFFFF);
        }
    }
}
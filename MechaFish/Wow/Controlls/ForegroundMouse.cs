using System.Diagnostics;
using System.Threading;
using System.Windows;
using MechaFish.Properties;
using MechaFish.Wow.ObjectManager;
using Point = System.Drawing.Point;

namespace MechaFish.Wow.Controlls {
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

            Point target = NativeMethods.ClientToScreen(GameManager.WindowHandle, objectClientPosition.X, objectClientPosition.Y);

            if (IsPointOutsideScreen(target)) {
                return false;
            }

            if (Settings.Default.MouseTeleport) {
                Position = target;
                Thread.Sleep(200);
                return wowObject.IsMouseOver;
            }

            Point start = Position;
            float t = 0;
            Stopwatch timer = new Stopwatch();

            timer.Start();
            long lastTime = timer.ElapsedMilliseconds;
            
            while (_position.X != target.X && _position.Y != target.Y) {
                var currentTime = timer.ElapsedMilliseconds;
                float deltaTime = (currentTime - lastTime);
                lastTime = currentTime;

                Position = Utils.MathUtils.MoveTowards(start, target, t);
                t += deltaTime;
            }
            Thread.Sleep(200);
            return wowObject.IsMouseOver;
        }

        public bool IsPointOutsideScreen(Point point) {
            if (point.X < 0) {
                return true;
            }

            if (point.X > SystemParameters.VirtualScreenWidth) {
                return true;
            }

            if (point.Y < 0) {
                return true;
            }

            if (point.Y > SystemParameters.VirtualScreenHeight) {
                return true;
            }

            return false;
        }
    }
}
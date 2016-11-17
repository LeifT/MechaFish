using System;
using System.Drawing;

namespace MasterAngler.Wow.Utils {
    public class MathUtils {

        public static float Clamp(float value, float min, float max) {
            if (value < min) {
                return min;
            }

            if (value > max) {
                return max;
            }

            return value;
        }

        public static float Lerp(float start, float target, float t) {
            t = Clamp(t, 0, 1);
            
            return start * (1.0f - t) + target * t;
        }
        

        public static Point Lerp(Point start, Point end, float t) {
            return new Point((int)Lerp(start.X, end.X, t), (int)Lerp(start.Y, end.Y, t));
        }

        public static float MoveTowards(float current, float target, float maxDelta) {
            if (Math.Abs(target - current) <= maxDelta) {
                return target;
            }

            return current + Math.Sign(target - current) * maxDelta;
        }

        public static Point MoveTowards(Point current, Point target, float maxDistanceDelta)
        {
            Point a = new Point(target.X - current.X, target.Y - current.Y);

            float magnitude = (float)Math.Sqrt(a.X * a.X + a.Y * a.Y);

            if (magnitude <= maxDistanceDelta || Math.Abs(magnitude) < float.Epsilon)
            {
                return target;
            }

            var b = a.X / magnitude;
            var c = a.Y / magnitude;

            return new Point((int)(current.X + b * maxDistanceDelta), (int)(current.Y + c * maxDistanceDelta));
        }
    }
}
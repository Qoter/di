using System;
using System.Drawing;

namespace TagCloud.Core.Infrastructure
{
    public static class PointExtension
    {
        public static double DistanceTo(this Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X)*(p1.X - p2.X) + (p1.Y - p2.Y)*(p1.Y - p2.Y));
        }

        public static Point Shift(this Point point, int shiftX, int shiftY)
        {
            return new Point(point.X + shiftX, point.Y + shiftY);
        }
    }
}

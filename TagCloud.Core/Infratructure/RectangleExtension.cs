using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Core.Infratructure
{
    public static class RectangleExtension
    {
        public static IEnumerable<Point> GetAngles(this Rectangle rectangle)
        {
            yield return new Point(rectangle.Left, rectangle.Top);
            yield return new Point(rectangle.Right, rectangle.Top);
            yield return new Point(rectangle.Right, rectangle.Bottom);
            yield return new Point(rectangle.Left, rectangle.Bottom);
        }

        public static double DistanceTo(this Rectangle rectanle, Point point)
        {
            return rectanle.GetAngles().Max(angle => angle.DistanceTo(point));
        }

        public static IEnumerable<Rectangle> CreateRectangles(Point anglePosition, Size rectangleSize)
        {
            return
                from shiftX in new[] { 0, -1 }
                from shiftY in new[] { 0, -1 }
                select new Rectangle(anglePosition.Shift(rectangleSize.Width * shiftX, rectangleSize.Height * shiftY), rectangleSize);
        }

        public static Rectangle Shift(this Rectangle rectangle, int xShift, int yShift)
        {
            rectangle.Offset(xShift, yShift);
            return rectangle;
        }

        public static IEnumerable<Rectangle> ShiftToFirstQuadrant(this IEnumerable<Rectangle> source)
        {
            var rects = source.ToList();
            var xShift = -rects.Min(rectangle => rectangle.Left);
            var yShift = -rects.Min(rectangle => rectangle.Top);

            return rects.Select(rectangle => rectangle.Shift(xShift, yShift));
        }

        public static Rectangle Resize(this Rectangle rectangle, double xResize, double yResize)
        {
            var x = (int)Math.Round(rectangle.X*xResize);
            var y = (int)Math.Round(rectangle.Y*yResize);

            var width = (int)Math.Round(rectangle.Width*xResize);
            var height = (int)Math.Round(rectangle.Height*yResize);

            return new Rectangle(x, y, width, height);
        }
    }
}

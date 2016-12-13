﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
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
    }
}
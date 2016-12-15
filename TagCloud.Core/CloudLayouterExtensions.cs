using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public static class CloudLayouterExtensions
    {
        public static Size CalculateSize(this ICloudLayouter layouter)
        {
            var minX = layouter.PlacedRectangles.Min(r => r.Left);
            var maxX = layouter.PlacedRectangles.Max(r => r.Right);
            var minY = layouter.PlacedRectangles.Min(r => r.Top);
            var maxY = layouter.PlacedRectangles.Max(r => r.Bottom);

            return new Size(maxX - minX, maxY - minY);
        }

        public static IEnumerable<Rectangle> ShiftToFirstQuadrant(this IEnumerable<Rectangle> source)
        {
            var rects = source.ToList();
            var xShift = -rects.Min(rectangle => rectangle.Left);
            var yShift = -rects.Min(rectangle => rectangle.Top);

            return rects.Select(rectangle => rectangle.Shift(xShift, yShift));
        }

        public static IEnumerable<Rectangle> ShiftToFirstQuadrant(this ICloudLayouter source)
        {
            return source.PlacedRectangles.ShiftToFirstQuadrant();
        }
    }
}

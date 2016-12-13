using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Infrastructure;

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

        public static IEnumerable<Rectangle> ShiftToFirstQuadrant(this ICloudLayouter layouter)
        {
            var xShift = -Math.Min(0, layouter.PlacedRectangles.Min(rectangle => rectangle.Left));
            var yShift = -Math.Min(0, layouter.PlacedRectangles.Min(rectangle => rectangle.Top));

            return layouter.PlacedRectangles.Select(rectangle => rectangle.Shift(xShift, yShift));
        }
    }
}

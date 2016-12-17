using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Domain
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

        public static IEnumerable<Rectangle> ShiftToFirstQuadrant(this ICloudLayouter source)
        {
            return source.PlacedRectangles.ShiftToFirstQuadrant();
        }
    }
}

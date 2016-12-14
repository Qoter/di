using System;
using System.Linq;
using TagCloud.Core;
using TagCloud.Core.Infratructure;

namespace TagCloud.CoreTests.Extensions
{
    public static class CircularCloudLayouterExtension
    {
        public static int GetTotalRectanglesSquare(this CircularCloudLayouter layouter)
        {
            return layouter.PlacedRectangles
                .Sum(rectangle => rectangle.Width*rectangle.Height);
        }

        public static double GetMinimalCircleRadius(this CircularCloudLayouter layouter)
        {
            return layouter.PlacedRectangles
                .SelectMany(RectangleExtension.GetAngles)
                .Max(angle => angle.DistanceTo(layouter.Center));
        }

        public static double GetMinimalCircleSquare(this CircularCloudLayouter layouter)
        {
            return Math.PI*Math.Pow(GetMinimalCircleRadius(layouter), 2);
        }

    }
}

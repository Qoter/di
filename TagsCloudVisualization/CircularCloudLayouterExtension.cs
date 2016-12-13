using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public static class CircularCloudLayouterExtension
    {
        public static int GetTotalRectanglesSquare(this CircularCloudLayouter layouter)
        {
            return layouter.Rectangles
                .Sum(rectangle => rectangle.Width*rectangle.Height);
        }

        public static double GetMinimalCircleRadius(this CircularCloudLayouter layouter)
        {
            return layouter.Rectangles
                .SelectMany(RectangleExtension.GetAngles)
                .Max(angle => angle.DistanceTo(layouter.Center));
        }

        public static double GetMinimalCircleSquare(this CircularCloudLayouter layouter)
        {
            return Math.PI*Math.Pow(layouter.GetMinimalCircleRadius(), 2);
        }

    }
}

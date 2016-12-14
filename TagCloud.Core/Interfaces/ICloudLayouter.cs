using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Core.Interfaces
{
    public interface ICloudLayouter
    {
        IEnumerable<Rectangle> PlacedRectangles { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
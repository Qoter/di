using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Core.Interfaces
{
    public interface IRectangleLayouter
    {
        IEnumerable<Rectangle> PlacedRectangles { get; }
        Rectangle PutNextRectangle(Size size);
    }
}
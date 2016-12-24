using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.Infratructure;

namespace TagCloud.Core.Interfaces
{
    public interface ICloudLayouter
    {
        IEnumerable<Rectangle> PlacedRectangles { get; }
        Result<Rectangle>PutNextRectangle(Size size);
    }
}
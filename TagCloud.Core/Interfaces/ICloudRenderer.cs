using System.Drawing;
using TagCloud.Core.Infratructure;

namespace TagCloud.Core.Interfaces
{
    public interface ICloudRenderer
    {
        Result<Bitmap> Render();
    }
}
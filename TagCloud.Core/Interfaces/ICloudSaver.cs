using TagCloud.Core.Infratructure;

namespace TagCloud.Core.Interfaces
{
    public interface ICloudSaver
    {
        Result<None> Save();
    }
}
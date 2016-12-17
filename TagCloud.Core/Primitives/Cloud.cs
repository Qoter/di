using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Core.Primitives
{
    public class Cloud
    {
        public readonly IReadOnlyList<Tag> Tags;
        public readonly Size Size;

        public Cloud(IEnumerable<Tag> tags, Size size)
        {
            Tags = tags.ToList().AsReadOnly();
            Size = size;
        }
    }
}

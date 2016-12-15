using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Primitives;

namespace TagCloud.Core
{
    public class TagCloud
    {
        public readonly IReadOnlyList<Tag> Tags;
        public readonly Size Size;

        public TagCloud(IEnumerable<Tag> tags, Size size)
        {
            Tags = tags.ToList().AsReadOnly();
            Size = size;
        }
    }
}

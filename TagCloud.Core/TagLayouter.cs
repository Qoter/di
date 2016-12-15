using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class TagLayouter
    { 
        public TagLayouter(IRectangleLayouter rectangleLayouter)
        {
            
        }

        public IEnumerable<Tag> PlacedTags { get; }
        public Tag PutNextTag(string word, Size tagSize)
        {
            return null;
        }
    }
}

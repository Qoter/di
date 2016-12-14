using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class Cloud
    {
        public IEnumerable<CloudItem> Items => items.AsReadOnly();

        private readonly ICloudLayouter layouter;
        private readonly List<CloudItem> items = new List<CloudItem>();

        public Cloud(ICloudLayouter layouter)
        {
            this.layouter = layouter;
        }

        public void PutWord(string word, Size size)
        {
            var area = layouter.PutNextRectangle(size);
            var item = new CloudItem(word, area);

            items.Add(item);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class Cloud
    {
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

        public Size CalculateSize()
        {
            var minX = layouter.PlacedRectangles.Min(r => r.Left);
            var maxX = layouter.PlacedRectangles.Max(r => r.Right);
            var minY = layouter.PlacedRectangles.Min(r => r.Top);
            var maxY = layouter.PlacedRectangles.Max(r => r.Bottom);

            return new Size(maxX - minX, maxY - minY);
        }

        public IEnumerable<CloudItem> GetItems()
        {
            return ShiftToFirstQuadrang(items);
        }

        private static IEnumerable<CloudItem> ShiftToFirstQuadrang(List<CloudItem> items)
        {
            var xShift = -Math.Min(0, items.Min(item => item.Area.Left));
            var yShift = -Math.Min(0, items.Min(item => item.Area.Top));

            return items.Select(item => new CloudItem(item.Word, item.Area.Shift(xShift, yShift)));
        }
    }
}
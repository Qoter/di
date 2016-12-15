using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class TagCloud
    {
        public IEnumerable<CloudItem> PlacedItems => items.AsReadOnly();

        private readonly ICloudLayouter layouter;
        private readonly List<CloudItem> items;

        public TagCloud(ICloudLayouter layouter)
        {
            this.items = new List<CloudItem>();
            this.layouter = layouter;
        }

        private TagCloud(IEnumerable<CloudItem> items)
        {
            this.items = items.ToList();
        }

        public void PutWord(string word, Size wordPlaceSize)
        {
            var place = layouter.PutNextRectangle(wordPlaceSize);
            var item = new CloudItem(word, place);

            items.Add(item);
        }

        public TagCloud Resize(Size newSize)
        {
            throw new NotImplementedException();
        }

        public Size CalculateSize()
        {
            var minX = layouter.PlacedRectangles.Min(r => r.Left);
            var maxX = layouter.PlacedRectangles.Max(r => r.Right);
            var minY = layouter.PlacedRectangles.Min(r => r.Top);
            var maxY = layouter.PlacedRectangles.Max(r => r.Bottom);

            return new Size(maxX - minX, maxY - minY);
        }

        private static IEnumerable<CloudItem> ShiftToFirstQuadrant(List<CloudItem> items)
        {
            var xShift = -Math.Min(0, items.Min(item => item.Place.Left));
            var yShift = -Math.Min(0, items.Min(item => item.Place.Top));

            return items.Select(item => new CloudItem(item.Word, item.Place.Shift(xShift, yShift)));
        }
    }
}
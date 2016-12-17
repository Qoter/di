using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;
using TagCloud.Core.Primitives;

namespace TagCloud.Core.Domain
{
    public class CloudBuilder
    {
        private static readonly Size DefaultCharSize = new Size(10, 10);
        private readonly ICloudLayouter layouter;
        private Func<string, Size> getWordSize = word => new Size(DefaultCharSize.Width * word.Length, DefaultCharSize.Height);
        private readonly List<string> placedWords = new List<string>();

        private CloudBuilder(ICloudLayouter layouter)
        {
            this.layouter = layouter;
        }

        public static CloudBuilder StartNew(ICloudLayouter layouter)
        {
            return new CloudBuilder(layouter);
        }

        public CloudBuilder WithWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                placedWords.Add(word);
                layouter.PutNextRectangle(getWordSize(word));
            }

            return this;
        }

        public CloudBuilder WithWordsSize(Func<string, Size> wordsSizeProvider)
        {
            getWordSize = wordsSizeProvider;
            return this;
        }

        public Cloud Build(Size cloudSize)
        {
            var tags = CreateTags(cloudSize);
            return new Cloud(tags, cloudSize);
        }

        public Cloud Build()
        {
            return Build(layouter.CalculateSize());
        }

        private IEnumerable<Tag> CreateTags(Size cloudSize)
        {
            var currentSize = layouter.CalculateSize();
            var widthResizeFactor = (double) cloudSize.Width/currentSize.Width;
            var heightResizeFactor = (double) cloudSize.Height/currentSize.Height;

            var places = layouter.PlacedRectangles
                .Select(rect => rect.Resize(widthResizeFactor, heightResizeFactor))
                .ShiftToFirstQuadrant();

            return placedWords.Zip(places, (word, place) => new Tag(word, place));
        }
    }
}
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
        private readonly ICloudSettingsProvider settingsProvider;
        private Func<string, Size> getWordSize = word => new Size(DefaultCharSize.Width * word.Length, DefaultCharSize.Height);
        private readonly List<string> placedWords = new List<string>();

        public CloudBuilder(ICloudLayouter layouter, ICloudSettingsProvider settingsProvider)
        {
            this.layouter = layouter;
            this.settingsProvider = settingsProvider;
        }

        public static CloudBuilder StartNew(ICloudLayouter layouter, ICloudSettingsProvider settingsProvider)
        {
            return new CloudBuilder(layouter, settingsProvider);
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

        public Cloud Build()
        {
            var tags = CreateTags(settingsProvider.CloudSettings.Size);
            return new Cloud(tags, settingsProvider.CloudSettings.Size);
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
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
        private Func<string, Result<Size>> getWordSize = word => new Size(DefaultCharSize.Width * word.Length, DefaultCharSize.Height).AsResult();
        private IEnumerable<string> words;

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
            this.words = words;
            return this;
        }

        public CloudBuilder WithWordsSize(Func<string, Result<Size>> wordsSizeProvider)
        {
            getWordSize = wordsSizeProvider;
            return this;
        }

        public Result<Cloud> Build()
        {
            return CreateTags(settingsProvider.CloudSettings.Size)
                .Then(tags => new Cloud(tags, settingsProvider.CloudSettings.Size));
        }

        private Result<IEnumerable<Tag>> CreateTags(Size cloudSize)
        {
            var placedWords = PlaceWords(layouter);
            if (!placedWords.IsSuccess)
                return Result.Fail<IEnumerable<Tag>>(placedWords.Error);

            var currentSize = layouter.CalculateSize();
            var widthResizeFactor = (double) cloudSize.Width/currentSize.Width;
            var heightResizeFactor = (double) cloudSize.Height/currentSize.Height;

            var places = layouter.PlacedRectangles
                .Select(rect => rect.Resize(widthResizeFactor, heightResizeFactor))
                .ShiftToFirstQuadrant();

            return placedWords.Value.Zip(places, (word, place) => new Tag(word, place)).AsResult();
        }

        private Result<List<string>> PlaceWords(ICloudLayouter cloudLayouter)
        {
            var placedWords = new List<string>();
            foreach (var word in words)
            {
                placedWords.Add(word);
                var placeResult = getWordSize(word).Then(cloudLayouter.PutNextRectangle);
                if (!placeResult.IsSuccess)
                    return Result.Fail<List<string>>(placeResult.Error);
            }

            return placedWords.AsResult();
        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.Core;
using TagCloud.Core.Infrastructure;
using TagCloud.CoreTests.Extensions;

namespace TagCloud.CoreTests
{
    [TestFixture]
    internal class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter defaultLayouter;
        private Size defaultSize;
        private Point defaultCenter;

        [SetUp]
        public void SetUp()
        {
            defaultCenter = new Point(0, 0);
            defaultLayouter = new CircularCloudLayouter(defaultCenter);
            defaultSize = new Size(10, 5);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;

            defaultLayouter.Normalize();
            var size = defaultLayouter.CalculateSize();
            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
            graphics.DrawRectangles(Pens.Black, defaultLayouter.Rectangles.ToArray());

            var savePath = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.Name + ".png");
            bitmap.Save(savePath, ImageFormat.Png);

            TestContext.Out.Write($"Tag cloud visualization saved to file {savePath}");
        }

        [Test, Pairwise]
        public void ThrowArgumentException_OnNotPositiveSize([Values(0, -1)]int width, [Values(0, -1)]int height)
        {
            Action tryPutNotPositiveSize = () => defaultLayouter.PutNextRectangle(new Size(width, height));

            tryPutNotPositiveSize.ShouldThrow<ArgumentException>();
        }


        [Test, Pairwise]
        public void PutRectangleSpecifiedSize([Values(1, 3)]int width, [Values(1, 3)]int height)
        {
            var specifiedSize = new Size(width, height);

            var rectangle = defaultLayouter.PutNextRectangle(specifiedSize);

            rectangle.Size.Should().Be(specifiedSize);
        }

        [Test, Pairwise]
        public void PutAlmostInTheCenter_FirstRectangle([Values(0, -1, 1, 3)]int centerX, [Values(0, -1, 1, 3)]int centerY)
        {
            var cloudCenter = new Point(centerX, centerY);
            var cloudLayouter = new CircularCloudLayouter(new Point(centerX, centerY));

            var rectangle = cloudLayouter.PutNextRectangle(defaultSize);

            rectangle.Location.DistanceTo(cloudCenter).Should().BeLessOrEqualTo(2);
        }

        [Test]
        public void PutRectanglesWithoutIntersects([Values(2, 3, 10)]int rectanglesCount)
        {
            defaultLayouter.PutAllRectangles(Enumerable.Repeat(defaultSize, rectanglesCount));

            (from first in defaultLayouter.Rectangles
             from second in defaultLayouter.Rectangles
             where first != second && first.IntersectsWith(second)
             select first).Should().BeEmpty();
        }

        [Test]
        public void SaveRectangles([Values(0, 1, 4)]int rectanglesCount)
        {
            var addedRectangles = defaultLayouter.PutAllRectangles(Enumerable.Repeat(defaultSize, rectanglesCount));

            defaultLayouter.Rectangles.Should().BeEquivalentTo(addedRectangles);
        }

        [Test]
        public void PutInCircleShape_IdenticalRectangles([Values(30, 100)]int rectanglesCount)
        {
            defaultLayouter.PutAllRectangles(Enumerable.Repeat(defaultSize, rectanglesCount));

            defaultLayouter.GetTotalRectanglesSquare().Should()
                .BeGreaterThan((int) (defaultLayouter.GetMinimalCircleSquare()/2));
        }

        [Test]
        public void ShiftRectanglesInFirstQuadrant_WhenCallNormalize([Values(1, 2, 50)]int rectanglesCount)
        {
            defaultLayouter.PutAllRectangles(Enumerable.Repeat(defaultSize, rectanglesCount));

            defaultLayouter.Normalize();

            defaultLayouter.Rectangles.Should().OnlyContain(rectangle => rectangle.X >= 0 && rectangle.Y >= 0);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
﻿using TagCloud.Core.Infrastructure;

namespace TagCloud.Core
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public IEnumerable<Rectangle> PlacedRectangles => placedRectangles.AsReadOnly();
        public Point Center { get; }

        private readonly List<Rectangle> placedRectangles = new List<Rectangle>();
        private const double DeltaAngle = 0.1;
        private const double SpiralFactor = 1/Math.PI;
        private double currentAngle = 0;

        public CircularCloudLayouter(Point center = default(Point))
        {
            Center = center;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException();

            var nextRectangle = PutNextRectangleOnSpiral(rectangleSize);
            placedRectangles.Add(nextRectangle);

            return nextRectangle;
        }

        public List<Rectangle> PutAllRectangles(IEnumerable<Size> sizes)
        {
            return sizes.Select(PutNextRectangle).ToList();
        }

        private Rectangle PutNextRectangleOnSpiral(Size rectangleSize)
        {
            return GenerateNextPoints()
                .Select(point => GetSuitableRectangles(point, rectangleSize).ToList())
                .First(rectanglesSet => rectanglesSet.Any())
                .MinBy(candidate => candidate.DistanceTo(Center));
        }

        private IEnumerable<Point> GenerateNextPoints()
        {
            while (true)
            {
                yield return GetNextPoint();
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private Point GetNextPoint()
        {
            currentAngle += DeltaAngle;
            var currentRadius = SpiralFactor*currentAngle;

            var currentX = currentRadius*Math.Cos(currentAngle);
            var currentY = currentRadius*Math.Sin(currentAngle);

            return new Point((int)Math.Round(currentX) + Center.X, (int)Math.Round(currentY) + Center.Y);
        }

        private IEnumerable<Rectangle> GetSuitableRectangles(Point nextPoint, Size rectangleSize)
        {
            return RectangleExtension
                .CreateRectangles(nextPoint, rectangleSize)
                .Where(CanPut);
        }

        private bool CanPut(Rectangle candidate)
        {
            return placedRectangles.All(placed => !placed.IntersectsWith(candidate));
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
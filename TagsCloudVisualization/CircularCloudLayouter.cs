﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public IReadOnlyList<Rectangle> Rectangles => placedRectangles.AsReadOnly();
        public Point Center { get; private set; }

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

        public void Normalize()
        {
            var xOffset = -Math.Min(0, placedRectangles.Min(rectangle => rectangle.Left));
            var yOffset = -Math.Min(0, placedRectangles.Min(rectangle => rectangle.Top));

            for (var i = 0; i < placedRectangles.Count; i++)
            {
                var rectangle = placedRectangles[i];
                rectangle.Offset(xOffset, yOffset);
                placedRectangles[i] = rectangle;
            }

            var center = Center;
            center.Offset(xOffset, yOffset);
            Center = center;
        }

        private Rectangle PutNextRectangleOnSpiral(Size rectangleSize)
        {
            return GenerateNextPoints()
                .Select(point => GetSuitableRectangles(point, rectangleSize).ToList())
                .First(rectanglesSet => rectanglesSet.Any())
                .MinBy(candidate => candidate.DistanceTo(Center));
        }

        public Size CalculateSize()
        {
            var minX = placedRectangles.Min(r => r.Left);
            var maxX = placedRectangles.Max(r => r.Right);
            var minY = placedRectangles.Min(r => r.Top);
            var maxY = placedRectangles.Max(r => r.Bottom);

            return new Size(maxX - minX, maxY - minY);
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

        private bool CanPut(Rectangle rectangle)
        {
            return placedRectangles.All(placed => !placed.IntersectsWith(rectangle));
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
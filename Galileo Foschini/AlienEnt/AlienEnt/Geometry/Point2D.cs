using System;

namespace AlienEnt.Geometry
{
    /// <summary>
    /// Describe the functionalities of a point in a 2D space.
    /// </summary>
    public class Point2D
    {
        /// <summary>
        /// The origin point: a point with x and y equals to 0.
        /// </summary>
        public static readonly Point2D ORIGIN = new(0, 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// the x value
        /// </summary>
        public double X { get; private set;}
        /// <summary>
        /// the y value
        /// </summary>
        public double Y { get; private set;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">another point</param>
        /// <returns>the distance between the points</returns>
        public double DistanceFrom(Point2D p) => Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));

        /// <inheritdoc/>
        public override string ToString() => $"Point2D [X = {X}, Y = {Y}]";

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Point2D d &&
                   X == d.X &&
                   Y == d.Y;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}
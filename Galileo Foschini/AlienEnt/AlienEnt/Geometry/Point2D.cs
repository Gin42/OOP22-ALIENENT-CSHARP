using System;

namespace AlienEnt.Geometry
{
    public class Point2D
    {
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; private set;}
        public double Y { get; private set;}

        public double DistanceFrom(Point2D p) => Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));

        public override string ToString() => $"Point2D [X = {X}, Y = {Y}]";

        public override bool Equals(object? obj)
        {
            return obj is Point2D d &&
                   X == d.X &&
                   Y == d.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static readonly Point2D ORIGIN = new(0, 0);
    }
}
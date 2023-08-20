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

        public double X { get; private set; };
        public double Y { get; private set;};

        public double distanceFrom(Point2D p) => Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));

        public override string ToString() => $"Point2D [X = {X}, Y = {Y}]";

        public override bool Equals(object obj) => obj is Point2D p &&
                    X == p.X &&
                    Y == p.Y;

        public override int GetHashCode()
        {
            //Codice generato tramite visual studio
            var hashCode = -2050752806;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static readonly Point2D ORIGIN = new Point2D(0, 0);
    }
}
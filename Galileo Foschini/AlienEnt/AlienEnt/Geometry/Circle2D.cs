using System;
using AlienEnt.Geometry;

namespace Geometry
{
    public class Circle2D
    {
        public Circle2D(Point2D center, double r)
        {
            Center = center;
            Ray = r;
        }

        public Point2D Center { get; private set; }
        public double Ray { get; private set; }

        public bool IntersectWith(Circle2D c) => Ray + c.Ray > Center.DistanceFrom(c.Center);

        //public bool IntersectWhith(Line2D l) =>

        public override string ToString()
        {
            return $"Circle2D[Center = {Center}; R = {Ray}]";
        }

        public override bool Equals(object? obj)
        {
            return obj is Circle2D d &&
                   EqualityComparer<Point2D>.Default.Equals(Center, d.Center) &&
                   Ray == d.Ray;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Center, Ray);
        }
    }
}
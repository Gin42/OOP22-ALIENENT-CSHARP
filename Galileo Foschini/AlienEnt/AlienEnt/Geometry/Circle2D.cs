using System;

namespace AlienEnt.Geometry
{
    /// <summary>
    /// Describe a circle in a 2D space.
    /// </summary>
    public class Circle2D
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="r"></param>
        public Circle2D(Point2D center, double r)
        {
            Center = center;
            Ray = r;
        }

        /// <summary>
        /// The center of the circle
        /// </summary>
        public Point2D Center { get; private set; }

        /// <summary>
        /// The Ray of the circle
        /// </summary>
        public double Ray { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns>true if the two cicle intersect with eachother, false otherwise</returns>
        public bool IntersectWith(Circle2D c) => Ray + c.Ray > Center.DistanceFrom(c.Center);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <returns>true if the circle intersect the line, false otherwise</returns>
        public bool IntersectWith(Line2D l) => Ray > l.DistancePoint(Center);

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Circle2D[Center = {Center}; R = {Ray}]";
        }
        
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Circle2D d &&
                   EqualityComparer<Point2D>.Default.Equals(Center, d.Center) &&
                   Ray == d.Ray;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Center, Ray);
    }
}
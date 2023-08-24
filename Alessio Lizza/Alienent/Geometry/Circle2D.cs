namespace Alienent.geometry
{
    public class Circle2D
    {
        private readonly Point2D _center;
        private readonly double _r;

        public Circle2D(Point2D center, double r)
        {
            _center = center;
            _r = r;
        }

        public Point2D GetCenter() => _center;

        public double GetRay() => _r;

        public bool IntersectWith(Circle2D c) => GetRay() + c.GetRay() > GetCenter().DistanceFrom(c.GetCenter());

        public bool IntersectWith(Line2D l) => GetRay() > l.DistancePoint(GetCenter());

        public override string ToString() => $"Circle2D [center={GetCenter()}, r={GetRay()}]";

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + (_center == null ? 0 : _center.GetHashCode());
            long temp;
            temp = BitConverter.DoubleToInt64Bits(_r);
            result = prime * result + (int)(temp ^ temp >> 32);
            return result;
        }

        public override bool Equals(object? obj)
        {
            return obj is Circle2D d &&
                EqualityComparer<Point2D>.Default.Equals(_center, d._center) &&
                _r == d._r;
        }
    }
}
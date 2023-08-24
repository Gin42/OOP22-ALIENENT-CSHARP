namespace Alienent{

    public class Point2D
    {
        public static readonly Point2D ORIGIN = new(0, 0);

        private readonly double x;
        private readonly double y;

        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double GetX() => x;

        public double GetY() => y;

        public double DistanceFrom(Point2D p) => Math.Sqrt(Math.Pow(this.GetX() - p.GetX(), 2) + Math.Pow(this.GetY() - p.GetY(), 2));

        public override string ToString() => $"Point2D [x={GetX()}, y={GetY()}]";

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(x);
            result = prime * result + (int)(temp ^ (temp >> 32));
            temp = BitConverter.DoubleToInt64Bits(y);
            result = prime * result + (int)(temp ^ (temp >> 32));
            return result;
        }

        public override bool Equals(object? obj)
        {
            return obj is Point2D d &&
                x == d.x &&
                y == d.y;
        }
    }
}
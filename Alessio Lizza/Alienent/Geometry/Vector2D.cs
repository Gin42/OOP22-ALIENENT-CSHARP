namespace Alienent.geometry
{
    public sealed class Vector2D
    {
        private const int ROUND_ANGLE = 360;
        private const int FLAT_ANGLE = 180;
        private const int POSITIVE_RIGHT = 90;
        private const int NEGATIVE_RIGHT = 270;

        public static readonly Vector2D NULL_VECTOR = new(0, 0);

        private readonly double _module;
        private readonly double _angle;

        private Vector2D(double angle, double module)
        {
            _angle = ConfineAngle(angle);
            _module = module;
        }

        public static Vector2D FromAngleAndModule(double angle, double module) => new(angle, module);

        public static Vector2D FromComponents(double xComp, double yComp)
        {
            double angle;
            double module = Math.Sqrt(xComp * xComp + yComp * yComp);
            if (xComp == 0)
            {
                if (yComp == 0)
                {
                    angle = 0;
                }
                else
                {
                    angle = yComp < 0 ? NEGATIVE_RIGHT : POSITIVE_RIGHT;
                }
            }
            else
            {
                double aTan = Math.Atan2(yComp, xComp);
                angle = xComp < 0 ? aTan * FLAT_ANGLE / Math.PI + FLAT_ANGLE : aTan * FLAT_ANGLE / Math.PI;
            }
            return new Vector2D(angle, module);
        }

        public static Vector2D FromTwoPoints(Point2D a, Point2D b) => FromComponents(b.GetX() - a.GetX(), b.GetY() - a.GetY());

        public static Vector2D FromTwoPointsAndModule(Point2D a, Point2D b, double module)
        {
            Vector2D vet = FromTwoPoints(a, b);
            return FromAngleAndModule(vet.GetAngle(), module);
        }

        public double GetXComp() => _module * Math.Cos(Math.PI * _angle / FLAT_ANGLE);

        public double GetYComp() => _module * Math.Sin(Math.PI * _angle / FLAT_ANGLE);

        public double GetAngle() => _angle;

        public double GetModule() => _module;

        public Point2D Translate(Point2D p) => new(p.GetX() + GetXComp(), p.GetY() + GetYComp());

        public Vector2D Add(Vector2D vec) => FromComponents(GetXComp() + vec.GetXComp(), GetYComp() + vec.GetYComp());

        public Vector2D Mul(double a) => FromAngleAndModule(_angle, _module * a);

        private static double ConfineAngle(double angle)
        {
            double newAngle = angle;
            bool condition = newAngle >= ROUND_ANGLE;
            while (condition)
            {
                newAngle -= ROUND_ANGLE;
                condition = newAngle >= ROUND_ANGLE;
            }
            condition = newAngle <= -ROUND_ANGLE;
            while (condition)
            {
                newAngle += ROUND_ANGLE;
                condition = newAngle <= -ROUND_ANGLE;
            }
            if (newAngle < 0)
            {
                newAngle = ROUND_ANGLE + newAngle;
            }
            return newAngle;
        }

        public override string ToString() => $"Vector2D [module={_module}, angle={_angle}]";

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(_module);
            result = prime * result + (int)(temp ^ temp >> 32);
            temp = BitConverter.DoubleToInt64Bits(_angle);
            result = prime * result + (int)(temp ^ temp >> 32);
            return result;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2D d &&
                   _module == d._module &&
                   _angle == d._angle;
        }
    }
}
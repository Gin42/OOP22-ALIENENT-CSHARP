using System;

namespace AlienEnt.Geometry
{
    public class Vector2D
    {
        private const int ROUND_ANGLE = 360;
        private const int FLAT_ANGLE = 180;
        private const int POSITIVE_RIGHT = 90;
        private const int NEGATIVE_RIGHT = 270;

        private Vector2D(double angle, double module)
        {
            Angle = ConfineAngle(angle);
            Module = module;
        }

        public double Module { get; private set; }
        public double Angle { get; private set; }
        public double XComp => Module * Math.Cos(ConvertToRadians(Angle));
        public double YComp => Module * Math.Sin(ConvertToRadians(Angle));

        public Point2D Translate(Point2D point) => new(point.X + XComp, point.Y + YComp);

        public Vector2D Add(Vector2D v) => FromComponents(XComp + v.XComp, YComp + v.YComp);

        public Vector2D Mul(double a) => FromAngleAndModule(Angle, Module * a);

        public override string ToString() => $"Vector2D[Angle = {Angle}; Module = {Module}]";

        public override bool Equals(object? obj)
        {
            return obj is Vector2D d &&
                   Module == d.Module &&
                   Angle == d.Angle &&
                   XComp == d.XComp &&
                   YComp == d.YComp;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Module, Angle, XComp, YComp);
        }

        public static Vector2D FromAngleAndModule(double angle, double module) => new(angle, module);

        public static Vector2D FromComponents(double xComp, double yComp)
        {
            double angle;
            double module = Math.Sqrt(xComp * xComp + yComp * yComp);
            if (xComp == 0)
            {
                if (yComp == 0)
                    angle = 0;
                else
                    angle = yComp < 0 ? NEGATIVE_RIGHT : POSITIVE_RIGHT;
            }
            else
            {
                double aTan = ConvertToDegrees(Math.Atan(yComp / xComp));
                angle = xComp < 0 ? aTan + FLAT_ANGLE : aTan;
            }
            return FromAngleAndModule(angle, module);
        }

        public static Vector2D FromTwoPoints(Point2D a, Point2D b) => FromComponents(b.X - a.X, b.Y - a.Y);

        public static Vector2D FromTwoPointsAndModule(Point2D a, Point2D b, double module) => 
                FromAngleAndModule(FromTwoPoints(a, b).Angle, module);

        private static double ConfineAngle(double angle)
        {
            double newAngle = angle;
            while (newAngle >= ROUND_ANGLE)
                newAngle -= ROUND_ANGLE;
            while (newAngle <= -ROUND_ANGLE)
                newAngle += ROUND_ANGLE;
            return newAngle >= 0 ? newAngle : newAngle + ROUND_ANGLE;
        }

        private static double ConvertToRadians(double angle) => Math.PI / FLAT_ANGLE * angle;

        private static double ConvertToDegrees(double angle) => angle * FLAT_ANGLE / Math.PI;
    }
}
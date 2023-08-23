using System;

namespace AlienEnt.Geometry
{
    /// <summary>
    /// Describe the functionalities of a Vector.
    /// </summary>
    public class Vector2D
    {
        private const int ROUND_ANGLE = 360;
        private const int FLAT_ANGLE = 180;
        private const int POSITIVE_RIGHT = 90;
        private const int NEGATIVE_RIGHT = 270;

        /// <summary>
        /// A vector with angle and module equals 0.
        /// </summary>
        public static readonly Vector2D NULL_VECTOR = new(0,0);

        private Vector2D(double angle, double module)
        {
            Angle = ConfineAngle(angle);
            Module = module;
        }

        /// <summary>
        /// The module of the vector
        /// </summary>
        public double Module { get; private set; }
        /// <summary>
        /// The angle of the vector
        /// </summary>
        public double Angle { get; private set; }
        /// <summary>
        /// the X component of the vector
        /// </summary>
        public double XComp => Module * Math.Cos(ConvertToRadians(Angle));
        /// <summary>
        /// the Y component of the vector
        /// </summary>
        public double YComp => Module * Math.Sin(ConvertToRadians(Angle));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns>the point resulting form the translation</returns>
        public Point2D Translate(Point2D point) => new(point.X + XComp, point.Y + YComp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns>return the vector obtainded adding vec</returns>
        public Vector2D Add(Vector2D v) => FromComponents(XComp + v.XComp, YComp + v.YComp);

        /// <summary>
        /// Multipy the vector for the given number
        /// </summary>
        /// <param name="a"></param>
        /// <returns>A new vector equal to the multiplication</returns>
        public Vector2D Mul(double a) => FromAngleAndModule(Angle, Module * a);

        /// <inheritdoc/>
        public override string ToString() => $"Vector2D[Angle = {Angle}; Module = {Module}]";

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Vector2D d &&
                   Module == d.Module &&
                   Angle == d.Angle &&
                   XComp == d.XComp &&
                   YComp == d.YComp;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Module, Angle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="module"></param>
        /// <returns>The vector with that angle and module</returns>
        public static Vector2D FromAngleAndModule(double angle, double module) => new(angle, module);

        /// <summary>
        /// Construct a Vector2D from the x and y components.
        /// </summary>
        /// <param name="xComp"></param>
        /// <param name="yComp"></param>
        /// <returns>a new Vector with the given components</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">starting point</param>
        /// <param name="b">ending point</param>
        /// <returns>the vector that goes from a and b</returns>
        public static Vector2D FromTwoPoints(Point2D a, Point2D b) => FromComponents(b.X - a.X, b.Y - a.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="module">the module of the resulting vector</param>
        /// <returns> the vector with the direction from a to b and the given module</returns>
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
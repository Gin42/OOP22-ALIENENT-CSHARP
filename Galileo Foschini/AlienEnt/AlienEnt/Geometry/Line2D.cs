using System;

namespace AlienEnt.Geometry
{
    /// <summary>
    /// represent a Line in a cartesian plane in the form ax+by+c=0.
    /// </summary>
    public class Line2D
    {
        /// <summary>
        /// A constructor that creates a line from the given components.
        /// To have a geometrical meaning at least one of a and b must be != 0.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <exception cref="ArgumentException"></exception>
        public Line2D(double a, double b, double c)
        {
            if(a == 0 && b == 0)
                throw new ArgumentException($"0x + 0y + {c}=0 doesn't represent a Line");
            A = a;
            B = b;
            C = c;
        }
        
        /// <summary>
        /// the value that multiply x
        /// </summary>
        public double A { get; private set;}
        /// <summary>
        /// the value that multiply y
        /// </summary>
        public double B { get; private set;}
        /// <summary>
        /// the known value
        /// </summary>
        public double C { get; private set;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns>the distance between the line and the given point.</returns>
        public double DistancePoint(Point2D p)
        {
            double num = Math.Abs(A * p.X + B * p.Y + C);
            double den = Math.Sqrt(A * A + B * B);
            return num / den;
        }

        /// <inheritdoc/>
        public override string ToString() => A + "x" + (B >= 0 ? "+" : "") + B + "y" + (C >= 0 ? "+" : "") + C;

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if(obj?.GetType() != typeof(Line2D))
                return false;

            Line2D line = (Line2D)obj;
            if (B == 0 && line.B == 0)
            {
                return C / A == line.C / line.A;
            }
            return A / B == line.A / line.B
                    && C / B == line.C / line.B;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(A, B, C);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>return the line passing through the given points.</returns>
        public static Line2D FromTwoPoints(Point2D p1, Point2D p2) => 
                new(p2.Y - p1.Y, p1.X - p2.X, p1.Y * p2.X - p1.X * p2.Y);

    }
}
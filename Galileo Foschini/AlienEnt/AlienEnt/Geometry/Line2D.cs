using System;

namespace AlienEnt.Geometry
{
    public class Line2D
    {

        public Line2D(double a, double b, double c)
        {
            if(a == 0 && b == 0)
                throw new ArgumentException($"0x + 0y + {c}=0 doesn't represent a Line");
            A = a;
            B = b;
            C = c;
        }
        
        public double A { get; private set;}
        public double B { get; private set;}
        public double C { get; private set;}

        public double DistancePoint(Point2D p)
        {
            double num = Math.Abs(A * p.X + B * p.Y + C);
            double den = Math.Sqrt(A * A + B * B);
            return num / den;
        }

        public override string ToString() => A + "x" + (B >= 0 ? "+" : "") + B + "y" + (C >= 0 ? "+" : "") + C;

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

        public override int GetHashCode() => HashCode.Combine(A, B, C);

        public static Line2D FromTwoPoints(Point2D p1, Point2D p2) => 
                new Line2D(p2.Y - p1.Y, p1.X - p2.X, p1.Y * p2.X - p1.X * p2.Y);

    }
}
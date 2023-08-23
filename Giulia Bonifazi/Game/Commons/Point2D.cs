namespace Commons {

    public class Point2D {
        public double X { get; set; }
        public double Y { get; set; }

        Point2D (double x, double y) {
            X = x;
            Y = y;
        }

        public static Point2D ORIGIN() => new(0.0, 0.0);
    }
}

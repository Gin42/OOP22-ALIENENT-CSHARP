namespace Commons {

    public class Point2D {
        private double X { get; set; }
        private double Y { get; set; }

        Point2D (double x, double y) {
            X = x;
            Y = y;
        }

        public static Point2D ORIGIN() => new Point2D (0.0, 0.0);
    }
}

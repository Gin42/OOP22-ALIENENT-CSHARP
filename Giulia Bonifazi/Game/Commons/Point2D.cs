namespace Commons {

    public class Point2D {
        public double X { get; set; }
        public double Y { get; set; }
        public static Point2D Origin 
        {
            get => new(0.0, 0.0);
        }

        public Point2D (double x, double y) {
            X = x;
            Y = y;
        }
    }
}

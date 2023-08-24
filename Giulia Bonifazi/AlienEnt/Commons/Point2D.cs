namespace AlienEnt.Commons {

    /// <summary>
    /// Utility class that represents a two dimensional point.
    /// </summary>
    public class Point2D
    {

        public Point2D (double x, double y) {
            X = x;
            Y = y;
        }
        
        public double X { get; set; }
        public double Y { get; set; }
        public static Point2D Origin 
        {
            get => new(0.0, 0.0);
        }

        /// <summary>
        /// This method checks if the two objects are equal in value.
        /// </summary>
        /// <param name="otherItem">
        /// The second object.
        /// </param>
        /// <returns>
        /// Whether the two objects are the same.
        /// </returns>
        public bool IsSame(object? otherItem)
        {
            if (otherItem is Point2D)
            {
                var item = otherItem as Point2D;
                if (item?.X == X && item?.Y == Y)
                {
                    return true;
                }
            }
            return false;
        }

    }
}

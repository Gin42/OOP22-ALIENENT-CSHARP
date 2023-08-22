using AlienEnt.Geometry;

namespace AlienEntTest.GeometryTest
{
    /// <summary>
    ///  Test Class for <c>Line2D</c>.
    /// </summary>
    [TestClass]
    public class Line2DTest
    {
        private readonly Line2D line1 = new(-1, 1, 0);
        private readonly Line2D line2 = new(0, 1, 0);

        /// <summary>
        /// Test if the constructor throws the correct Exeption.
        /// </summary>
        [TestMethod]
        public void ConstructorTest() {
            Assert.ThrowsException<ArgumentException>(() => new Line2D(0, 0, 0));
            Assert.ThrowsException<ArgumentException>(() => new Line2D(0, 0, 1));
        }

        /// <summary>
        /// Test the construction from two Points.
        /// </summary>
        [TestMethod]
        public void FromTwoPointsTest() {
            Point2D p1 = new(0, 0);
            Point2D p2 = new(1, 0);
            Point2D p3 = new(1, 1);
            Line2D x = Line2D.FromTwoPoints(p1, p2);
            Line2D xy = Line2D.FromTwoPoints(p1, p3);

            Assert.AreEqual(line1, xy);
            Assert.AreEqual(line2, x);

            Assert.ThrowsException<ArgumentException>(() => Line2D.FromTwoPoints(p1, p1));
        }
    }
}
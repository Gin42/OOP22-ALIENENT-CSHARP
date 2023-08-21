using AlienEnt.Geometry;

namespace AlienEntTest.GeometryTest
{
    /// <summary>
    /// Test Class for <c>Circle2D</c>.
    /// </summary>
    [TestClass]
    public class Circle2DTest
    {
        private const int P1 = 5;
        private const int P2 = 10;
        private const int RAY = 5;
        private readonly Circle2D circle1;
        private readonly Circle2D circle2;
        private readonly Circle2D circle3;

        public Circle2DTest() {
            this.circle1 = new Circle2D(Point2D.ORIGIN, RAY);
            this.circle2 = new Circle2D(new Point2D(P1, P1), RAY);
            this.circle3 = new Circle2D(new Point2D(P2, P2), RAY);
        }

        /// <summary>
        /// Test the <c>intersecWith</c>(Circle2D).
        /// </summary>
        [TestMethod]
        public void IntersectWithCircle2DTest() {
            Assert.IsTrue(circle1.IntersectWith(circle2));
            Assert.IsTrue(circle2.IntersectWith(circle1));
            Assert.IsFalse(circle1.IntersectWith(circle3));
            Assert.IsFalse(circle3.IntersectWith(circle1));
        }

        /// <summary>
        /// Test the <c>intersecWith</c>(Line2D).
        /// </summary>
        [TestMethod]
        public void IntersectWithLine2DTest() {
            Line2D line1 = new(1, 0, 0);
            Line2D line2 = new(-1, 1, 0);

            Assert.IsTrue(circle1.IntersectWith(line1));
            Assert.IsFalse(circle2.IntersectWith(line1));
            Assert.IsFalse(circle3.IntersectWith(line1));

            Assert.IsTrue(circle1.IntersectWith(line2));
            Assert.IsTrue(circle2.IntersectWith(line2));
            Assert.IsTrue(circle3.IntersectWith(line2));
        }
    }
}
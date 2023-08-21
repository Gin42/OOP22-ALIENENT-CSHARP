using System;
using AlienEnt.Geometry;

namespace AlienEntTest.GeometryTest
{
    /// <summary>
    /// Test Class <c>for Point2D</c>.
    /// </summary>
    [TestClass]
    public class Point2DTest
    {
        private const double TOLL = 0.01;

        /// <summary>
        ///  Test the DistanceFrom method.
        /// </summary>
        [TestMethod]
        public void DistanceTest()
        {
            var p1 = Point2D.ORIGIN;
            var p2 = new Point2D(3, 4);
            var p3 = new Point2D(10,0);

            Assert.AreEqual(5,p1.DistanceFrom(p2),TOLL);
            Assert.AreEqual(10,p1.DistanceFrom(p3),TOLL);
            Assert.IsTrue(p2.DistanceFrom(p3) < 9);
            Assert.IsTrue(p2.DistanceFrom(p3) > 8);
        }
    }
}
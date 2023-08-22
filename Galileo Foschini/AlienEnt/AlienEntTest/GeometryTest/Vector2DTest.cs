using AlienEnt.Geometry;

namespace AlienEntTest.GeometryTest
{
    /// <summary>
    /// Test Class for <c>Vector2D</c>.
    /// </summary>
    [TestClass]
    public class Vector2DTest
    {
        private const double TOLL = 0.01;

        /// <summary>
        /// Test the construction of a <c>Vector2D</c> From Angle and Module.
        /// </summary>
        [TestMethod]
        public void FromAngleAndModuleTest() {
            Vector2D vec = Vector2D.FromAngleAndModule(45, 1);
            Assert.AreEqual(45, vec.Angle);
            Assert.AreEqual(1, vec.Module);

            vec = Vector2D.FromAngleAndModule(380, 1);
            Assert.AreEqual(20, vec.Angle);

            vec = Vector2D.FromAngleAndModule(-380, 1);
            Assert.AreEqual(340, vec.Angle);
        }

        /// <summary>
        /// Test the construction of a <c>Vector2D</c> from the components.
        /// </summary>
        [TestMethod]
        public void FromComponentsTest() {
            Vector2D vec1 = Vector2D.FromAngleAndModule(90, 1);
            Vector2D test = Vector2D.FromComponents(0, 1);
            Assert.AreEqual(vec1, test);
            Assert.AreEqual(0, test.XComp, TOLL);
            Assert.AreEqual(1, test.YComp, TOLL);

            vec1 = Vector2D.FromAngleAndModule(0, 1);
            test = Vector2D.FromComponents(1, 0);
            Assert.AreEqual(vec1, test);
            Assert.AreEqual(1, test.XComp, TOLL);
            Assert.AreEqual(0, test.YComp, TOLL);

            vec1 = Vector2D.FromAngleAndModule(180, 1);
            test = Vector2D.FromComponents(-1, 0);
            Assert.AreEqual(vec1, test);
            Assert.AreEqual(-1, test.XComp, TOLL);
            Assert.AreEqual(0, test.YComp, TOLL);
        }

        /// <summary>
        /// Test the construction of a <c>Vector2D</c> From Two Points.
        /// </summary>
        [TestMethod]
        public void FromTwoPointsTest() {
            Point2D a = Point2D.ORIGIN;
            Point2D b = new(345, 25);
            Vector2D vec = Vector2D.FromTwoPoints(a, b);
            Assert.AreEqual(b.X, vec.Translate(a).X, TOLL);
            Assert.AreEqual(b.Y, vec.Translate(a).Y, TOLL);
        }

        /// <summary>
        /// Test the construction of a <c>Vector2D</c> From Two Points and module.
        /// </summary>
        [TestMethod]
        public void FromTwoPointsAndModuleTest() {
            Point2D a = new(0, 0);
            Point2D b = new(345, 0);
            Vector2D vec = Vector2D.FromTwoPointsAndModule(a, b, 1);
            Assert.AreEqual(b, vec.Mul(345).Translate(a));
        }

        /// <summary>
        /// Test the <c>Translate</c> method.
        /// </summary>
        [TestMethod]
        public void TranslatePointTest() {
            var vec1 = Vector2D.FromComponents(5, 5);
            var vec2 = Vector2D.FromComponents(-2, -1);
            var vec3 = Vector2D.FromComponents(-1, 2);

            Point2D a = new(0, 0);

            Point2D b = new(5, 5);
            Assert.AreEqual(b.X, vec1.Translate(a).X, TOLL);
            Assert.AreEqual(b.Y, vec1.Translate(a).Y, TOLL);

            b = new Point2D(-2, -1);
            Assert.AreEqual(b.X, vec2.Translate(a).X, TOLL);
            Assert.AreEqual(b.Y, vec2.Translate(a).Y, TOLL);

            b = new Point2D(-1, 2);
            Assert.AreEqual(b.X, vec3.Translate(a).X, TOLL);
            Assert.AreEqual(b.Y, vec3.Translate(a).Y, TOLL);

            b = new Point2D(2, 6);
            var at = vec3.Translate(vec2.Translate(vec1.Translate(a)));
            Assert.AreEqual(b.X, at.X, TOLL);
            Assert.AreEqual(b.Y, at.Y, TOLL);
        }
    }
}
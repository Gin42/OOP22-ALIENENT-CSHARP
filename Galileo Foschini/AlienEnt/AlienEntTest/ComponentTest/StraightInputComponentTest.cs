using AlienEnt.GameObject;
using AlienEnt.GameObject.Component;
using AlienEnt.Geometry;

namespace AlienEntTest.ComponentTest
{
    [TestClass]
    public class StraightInputComponentTest
    {
        private const double TOLL = 0.01;

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(34)]
        public void TestMovement(double deltaTime)
        {
            IGameObject gameObject = new GameObject(
                        Point2D.ORIGIN,
                        Vector2D.FromAngleAndModule(0, 10),
                        new Dictionary<Statistic, int>(),
                        "obj"
                    );
            StraightInputComponent inputComponent = new(gameObject, true);
            gameObject.AddComponent(inputComponent);

            inputComponent.Update(deltaTime);
            Assert.AreEqual(10 * deltaTime, gameObject.Position.X, TOLL);
            Assert.AreEqual(0, gameObject.Position.Y, TOLL);
        }
    } 
}
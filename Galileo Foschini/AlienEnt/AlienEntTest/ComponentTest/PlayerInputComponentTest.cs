using AlienEnt.GameObject;
using AlienEnt.GameObject.Component;
using AlienEnt.GameObject.Component.Api;
using AlienEnt.Geometry;
using static AlienEnt.GameObject.IInputSupplier;

namespace AlienEntTest.ComponentTest
{
    [TestClass]
    public class PlayerInputComponentTest
    {
        private const double TOLL = 0.01;

        private readonly IGameObject _ship;
        private readonly IPlayerInputComponent _playerInputComponent;
        private readonly IInputSupplier _inputSupplier;

        public PlayerInputComponentTest()
        {
            _ship = new GameObject(
                        Point2D.ORIGIN, 
                        Vector2D.FromAngleAndModule(90,0),
                        new Dictionary<Statistic, int>
                        {
                            {Statistic.SPEED, 60},
                            {Statistic.DAMAGE, 10},
                            {Statistic.PROJECTILESPEED, 20},
                            {Statistic.COOlDOWN, 1}
                        },
                        "ship"
                    );
            _inputSupplier = new InputSupplier();
            _playerInputComponent = new PlayerInputComponent(_ship, _inputSupplier);
            _playerInputComponent.Start();
        }

        [TestMethod]
        public void MovementTest()
        {
            _inputSupplier.AddInput(Input.ACCELERATE);

            _playerInputComponent.Update(1);
            Assert.AreEqual(60, _ship.Velocity.Module, TOLL);
            Assert.AreEqual(90, _ship.Velocity.Angle, TOLL);
            Assert.AreEqual(0, _ship.Position.X, TOLL);
            Assert.AreEqual(60, _ship.Position.Y, TOLL);

            Reset();

            _inputSupplier.AddInput(Input.ACCELERATE);
            _playerInputComponent.Update(0.1);
            Assert.AreEqual(60 * 3 * 0.1,_ship.Velocity.Module, TOLL);
            Assert.AreEqual(90, _ship.Velocity.Angle, TOLL);
            Assert.AreEqual(0, _ship.Position.X, TOLL);
            Assert.AreEqual(60 * 3 * 0.1 * 0.1, _ship.Position.Y, TOLL);
            Reset();
        }

        [TestMethod]
        public void TurningTest()
        {
            _inputSupplier.AddInput(Input.TURN_LEFT);
            _playerInputComponent.Update(0.25);
            Assert.AreEqual(0, _ship.Velocity.Angle, TOLL);
            Assert.AreEqual(Point2D.ORIGIN, _ship.Position);

            Reset();

            _inputSupplier.AddInput(Input.TURN_RIGHT);
            _playerInputComponent.Update(0.25);
            Assert.AreEqual(180, _ship.Velocity.Angle, TOLL);
            Assert.AreEqual(Point2D.ORIGIN, _ship.Position);

            Reset();
        }

        [TestMethod]
        public void StoppingTest()
        {
            _inputSupplier.AddInput(Input.ACCELERATE);
            _playerInputComponent.Update(1);

            _inputSupplier.AddInput(Input.STOP_ACCELERATE);
            _playerInputComponent.Update(0.1);
            Assert.AreEqual(42, _ship.Velocity.Module, TOLL);
            Assert.AreEqual(90, _ship.Velocity.Angle, TOLL);

            _inputSupplier.AddInput(Input.STOP_ACCELERATE);
            _playerInputComponent.Update(1);
            Assert.AreEqual(0, _ship.Velocity.Module, TOLL);
            Assert.AreEqual(90, _ship.Velocity.Angle, TOLL);

            Reset();
        }

        private void Reset()
        {
            _ship.Position = Point2D.ORIGIN;
            _ship.Velocity = Vector2D.FromAngleAndModule(90,0);

            _inputSupplier.ClearInputs();
        }
    }
}
using AlienEnt.GameObject;
using AlienEnt.GameObject.Component;
using AlienEnt.GameObject.Component.Api;
using AlienEnt.Geometry;

namespace AlienEntTest.ComponentTest
{
    [TestClass]
    public class BasicShooterComponentTest
    {
        private readonly IGameObject _ship;
        private readonly Func<IGameObject> _supplier;
        private readonly List<IGameObject> _world = new();

        public BasicShooterComponentTest()
        {
            _ship = new GameObject(
                        new Point2D(1,1), 
                        Vector2D.FromAngleAndModule(90,0),
                        new Dictionary<Statistic, int>
                        {
                            {Statistic.DAMAGE, 10},
                            {Statistic.PROJECTILESPEED, 20},
                            {Statistic.COOlDOWN, 1}
                        },
                        "ship"
                    );
            _ship.AddComponent(new ExampleHitboxComponent(_ship, IHitboxComponent.GameObjectType.PLAYER));
            _supplier = () => 
                {
                    var stat = new Dictionary<Statistic, int>
                    {
                        { Statistic.HP, 1 }
                    };
                    var o = new GameObject(Point2D.ORIGIN, Vector2D.NULL_VECTOR, stat, "simpleProjectile");
                    o.AddComponent(new ExampleProjectileHitboxComponent(o));
                    _world.Add(o);
                    return o;
                };
        }

        [TestMethod]
        public void ShootingTest()
        {
            BasicShooterComponent shooterComponent = new(_ship, true, _supplier);
            shooterComponent.Start();

            shooterComponent.Shoot();
            shooterComponent.Update(0);
            Assert.IsTrue(_world.Count == 0);

            shooterComponent.Update(1);
            Assert.IsTrue(_world.Count == 0);

            shooterComponent.Shoot();
            shooterComponent.Update(0);
            Assert.IsTrue(_world.Count == 1);

            shooterComponent.Shoot();
            shooterComponent.Update(0);
            Assert.IsTrue(_world.Count == 1);

            _world.Clear();
        }

        [TestMethod]
        public void ProjectilePropertyTest()
        {
            BasicShooterComponent shooterComponent = new(_ship, true, _supplier);
            shooterComponent.Start();

            shooterComponent.Shoot();
            shooterComponent.Update(1);
            var projectile = _world.First();

            Assert.AreEqual(10, projectile.GetStatValue(Statistic.DAMAGE));
            Assert.AreEqual(20, projectile.GetStatValue(Statistic.SPEED));

            Assert.AreEqual(_ship.Position, projectile.Position);
            Assert.AreEqual(Vector2D.FromAngleAndModule(90, 20), projectile.Velocity);

            var hb = projectile.GetComponent<IProjectileHitboxComponent>();
            Assert.IsNotNull(hb);
            Assert.AreEqual(IHitboxComponent.GameObjectType.PLAYER, hb.ShooterType);

            _world.Clear(); 
        }
    }
}
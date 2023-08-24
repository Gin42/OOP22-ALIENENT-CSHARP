using AlienEnt.GameObject;
using AlienEnt.GameObject.Component;
using AlienEnt.GameObject.Component.Api;
using AlienEnt.Geometry;

namespace AlienEntTest
{
    [TestClass]
    public class GameObjectTest
    {
        private const double TOLL = 0.01;
        [TestMethod]
        public void ContructionTest()
        {
            var stat = new Dictionary<Statistic, int>
                {
                    {Statistic.HP, 10},
                    {Statistic.RECOVERY, 1},
                };
            var gameObject = new GameObject(
                Point2D.ORIGIN,
                Vector2D.NULL_VECTOR,
                stat,
                "gameObject"
                );
            Assert.AreEqual(10, gameObject.GetStatValue(Statistic.HP));
            Assert.AreEqual(1, gameObject.GetStatValue(Statistic.RECOVERY));

            Assert.AreEqual(10, gameObject.GetHealth());

            stat[Statistic.HP] = 2;
            Assert.AreNotEqual(stat[Statistic.HP], gameObject.GetStatValue(Statistic.HP));
        }

        [TestMethod]
        public void ComponentTest()
        {
            var gameObject = new GameObject(
                Point2D.ORIGIN,
                Vector2D.FromComponents(0,1),
                new Dictionary<Statistic, int>
                {
                    {Statistic.HP, 10},
                    {Statistic.RECOVERY, 1},
                    {Statistic.SPEED, 1}
                },
                "gameObject"
                );
            var input = new StraightInputComponent(gameObject, true);
            gameObject.AddComponent(input);

            Assert.IsTrue(gameObject.GetAllComponents().Count != 0);
            Assert.AreEqual(input, gameObject.GetComponent<IInputComponent>());

            gameObject.Update(1);
            Assert.AreEqual(0, gameObject.Position.X, TOLL);
            Assert.AreEqual(1, gameObject.Position.Y, TOLL);

            var shooter = new BasicShooterComponent(gameObject, true, () => gameObject);
            var powerUp = new PlayerPowerUpComponent(gameObject);

            gameObject.AddAllComponents(new List<IComponent>(){shooter, powerUp});
            Assert.AreEqual(3, gameObject.GetAllComponents().Count);
            Assert.AreEqual(shooter, gameObject.GetComponent<IShooterComponent>());
            Assert.AreEqual(powerUp, gameObject.GetComponent<IPowerUpComponent>());
        }

        [TestMethod]
        public void HealtTest()
        {
            var gameObject = new GameObject(
                Point2D.ORIGIN,
                Vector2D.FromComponents(0,1),
                new Dictionary<Statistic, int>
                {
                    {Statistic.HP, 10},
                    {Statistic.RECOVERY, 1},
                    {Statistic.SPEED, 1}
                },
                "gameObject"
                );
            
            var health = gameObject.GetHealth();
            gameObject.Heal(1);
            Assert.AreEqual(health, gameObject.GetHealth());

            health = gameObject.GetHealth();
            gameObject.Hit(5);
            Assert.AreEqual(health-5, gameObject.GetHealth());

            health = gameObject.GetHealth();
            gameObject.Heal(3);
            Assert.AreEqual(health + 3, gameObject.GetHealth());

            health = gameObject.GetHealth();
            gameObject.Recovery(1);
            Assert.AreEqual(health + 1, gameObject.GetHealth());

            Assert.IsTrue(gameObject.IsAlive());

            gameObject.Hit(gameObject.GetHealth());
            Assert.IsFalse(gameObject.IsAlive());
        }

        [TestMethod]
        public void StatsTest()
        {
            var gameObject = new GameObject(
                Point2D.ORIGIN,
                Vector2D.FromComponents(0,1),
                new Dictionary<Statistic, int>
                {
                    {Statistic.HP, 10},
                    {Statistic.RECOVERY, 1},
                    {Statistic.SPEED, 1}
                },
                "gameObject"
                );
            
            gameObject.SetStatValue(Statistic.RECOVERY, 20);
            Assert.AreEqual(20, gameObject.GetStatValue(Statistic.RECOVERY));
            
            gameObject.SetStatValue(Statistic.DEFENCE, 20);
            Assert.AreEqual(20, gameObject.GetStatValue(Statistic.RECOVERY));
        }
    }
}
using AlienEnt.GameObject;
using AlienEnt.GameObject.Component;
using AlienEnt.GameObject.Component.Api;
using AlienEnt.Geometry;

namespace AlienEntTest.ComponentTest
{
    [TestClass]
    public class PlayerPowerUpComponentTest
    {

        [TestMethod]
        public void PowerUpTest(){
            var stats = CreateStats();
            var ship = CreateGameObject(stats);
            IPowerUpComponent powerUpComponent = new PlayerPowerUpComponent(ship);
            
            Dictionary<Statistic, int> powerUps = new()
                    {
                        {Statistic.HP, 10},
                        {Statistic.SPEED, 10},
                        {Statistic.DAMAGE, 10},
                        {Statistic.DEFENCE, 10},
                        {Statistic.PROJECTILESPEED, 10},
                        {Statistic.COOlDOWN, 10},
                        {Statistic.RECOVERY, 10}
                    };
            
            powerUpComponent.SetPowerUps(powerUps);
            Assert.AreEqual(110, ship.GetStatValue(Statistic.HP));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.SPEED));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.DAMAGE));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.DEFENCE));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.PROJECTILESPEED));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.COOlDOWN));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.RECOVERY));
        }

        [TestMethod]
        public void MissingPowerUp()
        {
            var stats = CreateStats();
            var ship = CreateGameObject(stats);
            IPowerUpComponent powerUpComponent = new PlayerPowerUpComponent(ship);
            
            Dictionary<Statistic, int> powerUps = new()
                    {
                        {Statistic.HP, 10},
                        {Statistic.SPEED, 10},
                        {Statistic.DAMAGE, 10},
                        {Statistic.DEFENCE, 10},
                        {Statistic.PROJECTILESPEED, 10},
                        {Statistic.COOlDOWN, 10},
                    };
            
            powerUpComponent.SetPowerUps(powerUps);
            Assert.AreEqual(110, ship.GetStatValue(Statistic.HP));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.SPEED));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.DAMAGE));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.DEFENCE));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.PROJECTILESPEED));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.COOlDOWN));
            Assert.AreEqual(100, ship.GetStatValue(Statistic.RECOVERY));

        }

        [TestMethod]
        public void MissingStat()
        {
            var stats = CreateStats();
            stats.Remove(Statistic.RECOVERY);

            var ship = CreateGameObject(stats);
            IPowerUpComponent powerUpComponent = new PlayerPowerUpComponent(ship);
            
            Dictionary<Statistic, int> powerUps = new()
                    {
                        {Statistic.HP, 10},
                        {Statistic.SPEED, 10},
                        {Statistic.DAMAGE, 10},
                        {Statistic.DEFENCE, 10},
                        {Statistic.PROJECTILESPEED, 10},
                        {Statistic.COOlDOWN, 10},
                        {Statistic.RECOVERY, 10}
                    };
            
            powerUpComponent.SetPowerUps(powerUps);
            Assert.AreEqual(110, ship.GetStatValue(Statistic.HP));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.SPEED));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.DAMAGE));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.DEFENCE));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.PROJECTILESPEED));
            Assert.AreEqual(110, ship.GetStatValue(Statistic.COOlDOWN));
        }

        [TestMethod]
        public void HealthChangeTest()
        {
            var stats = CreateStats();
            var ship = CreateGameObject(stats);
            IPowerUpComponent powerUpComponent = new PlayerPowerUpComponent(ship);
            
            Dictionary<Statistic, int> powerUps = new()
                    {
                        {Statistic.HP, 10},
                    };
            
            powerUpComponent.SetPowerUps(powerUps);
            Assert.AreEqual(110, ship.GetHealth());

            ship = CreateGameObject(stats);
            powerUpComponent = new PlayerPowerUpComponent(ship);

            powerUps = new()
                    {
                        {Statistic.HP, -10},
                    };
            
            powerUpComponent.SetPowerUps(powerUps);
            Assert.AreEqual(90, ship.GetHealth());
        }
        private static IGameObject CreateGameObject(Dictionary<Statistic, int> stats)
        {
            return new GameObject(
                        Point2D.ORIGIN, 
                        Vector2D.NULL_VECTOR,
                        stats,
                        "ship"
                    );
        }

        private static Dictionary<Statistic, int> CreateStats()
        {
            return new Dictionary<Statistic, int>
                        {
                            {Statistic.HP, 100},
                            {Statistic.SPEED, 100},
                            {Statistic.DAMAGE, 100},
                            {Statistic.DEFENCE, 100},
                            {Statistic.PROJECTILESPEED, 100},
                            {Statistic.COOlDOWN, 100},
                            {Statistic.RECOVERY, 100}
                        };
        }
    }
}
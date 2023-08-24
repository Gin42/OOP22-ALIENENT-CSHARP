using AlienEnt.Commons;
using AlienEnt.Commons.Bounds;
using AlienEnt.GameWorld;
using AlienEnt.Props;

namespace AlienEntTest
{
    [TestClass]
    public class GameWorldTest
    {
        private static readonly Dictionary<PropStatistic, int> s_stats = new()
        {
            {PropStatistic.HP, 10}
        };
        private readonly IWorld _world;
        private readonly IDimensions _dimensions;

        public GameWorldTest()
        {
            _dimensions = new Dimensions(200, 200);
            _world = new World(_dimensions);
        }

        [TestMethod]
        public void ScoreAndEnemyCountTest()
        {
            PropGameObject enemy1 = new(Point2D.Origin, s_stats, "enemy");
            PropGameObject player = new(new(20, 20), s_stats, "player");
            PropGameObject enemy2 = new(new(20, 30), s_stats, "enemy");
            _world.Player = player;
            _world.AddAllGameObjects(enemy1, enemy2);

            Assert.AreEqual(0, _world.Score);
            Assert.AreEqual(2, _world.EnemyCount);

            enemy1.Hp = 0;
            player.Hp = 0;
            _world.Update(0.0);
            Assert.AreEqual(enemy1.Hp * 100, _world.Score);
            Assert.AreEqual(1, _world.EnemyCount);

            enemy2.Hp = 0;
            _world.Update(1.0);
            Assert.AreEqual((enemy2.Hp + enemy1.Hp) * 100, _world.Score);
            Assert.AreEqual(0, _world.EnemyCount);
            Assert.IsTrue(_world.IsOver);
            _world.AddGameObject(new(Point2D.Origin, s_stats, "enemy"));
        }

        [TestMethod]
        public void IsOverTest()
        {
            PropGameObject enemy = new(Point2D.Origin, s_stats, "enemy");
            PropGameObject player = new(new(20, 20), s_stats, "player");
            _world.Player = player;
            _world.AddGameObject(enemy);
            Assert.IsFalse(_world.IsOver);

            enemy.Hp = 0;
            _world.Update(0.0);
            Assert.IsFalse(_world.IsOver);

            player.Hp = 0;
            _world.Update(0.0);
            Assert.IsTrue(_world.IsOver);
        }

        [TestMethod]
        public void TestLastAdded()
        {
            Assert.AreEqual(_world.LastAdded.Count, 0);

            PropGameObject obj1 = new(Point2D.Origin, s_stats, "enemy");
            PropGameObject obj2 = new(new(20, 20), s_stats, "player");
            PropGameObject obj3 = new(new(20, 30), s_stats, "enemy");
            var list = new List<PropGameObject>(){ obj1, obj2, obj3 };
            _world.AddAllGameObjects(obj1, obj2, obj3);
            Assert.IsTrue(list.SequenceEqual(_world.LastAdded));
            Assert.AreEqual(_world.LastAdded.Count, 0);
        }
    }
}
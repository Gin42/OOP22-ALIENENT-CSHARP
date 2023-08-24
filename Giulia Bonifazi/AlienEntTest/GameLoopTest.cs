using AlienEnt.Commons.Bounds;
using AlienEnt.Commons.Queue;
using AlienEnt.GameWorld;
using AlienEnt.MainLoop;
using AlienEnt.Props;

namespace AlienEntTest
{
    [TestClass]
    public class GameLoopTest
    {
        private readonly GameLoop _gameLoop;
        private readonly CancellationTokenSource _cts;
        private readonly PropRendererManager _rendererManager;
        private readonly IWorld _world;

        public GameLoopTest()
        {
            _world = new World(new Dimensions());
            _rendererManager = new PropRendererManager();
            _cts = new CancellationTokenSource();
            _gameLoop = new GameLoop(new InputQueue(1), _rendererManager, _world,
                                             "player", _cts.Token);
        }

        [TestMethod]
        public void PauseTest()
        {
            int enemySpawnTimeMillis = (int) PropEnemySpawner.s_enemySpawnTime * 1000;

            // Start the loop and let it work for a bit
            _gameLoop.StartLoop();
            // This expression makes sure the GameLoop always spawns one enemy
            // and that no enemy is spawned before the pause.
            Thread.Sleep(enemySpawnTimeMillis * 2 - enemySpawnTimeMillis / 3);
            
            // Remember how many enemies had spawned before the pause.
            _gameLoop.PauseLoop();
            int exp = _rendererManager.RendererCount;
            Thread.Sleep(enemySpawnTimeMillis * 5);

            // Check that the number of enemies stayed the same through the pause.
            Assert.AreEqual(exp, _rendererManager.RendererCount);

            // Resume the loop and check that the enemies keep spawning.
            _gameLoop.ResumeLoop();
            Thread.Sleep(enemySpawnTimeMillis * 2);
            Assert.IsTrue(exp < _rendererManager.RendererCount);
        }

        [TestMethod]
        public void StopTest()
        {
            int waitForThreadDeath = 5000;

            _gameLoop.StartLoop();
            _cts.Cancel();
            // It might take a while for the thread to die.
            Thread.Sleep(waitForThreadDeath);
            Assert.IsFalse(_gameLoop.IsAlive());
        }
    }
}
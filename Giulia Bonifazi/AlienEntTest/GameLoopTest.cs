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
            // Start the loop and let it work for a bit
            _gameLoop.StartLoop();
            Thread.Sleep(1000);
            
            // Remember how many enemies had spawned before the pause.
            int exp = _rendererManager.RendererCount;
            _gameLoop.PauseLoop();
            Thread.Sleep(2000);

            // Check that the number of enemies stayed the same through the pause.
            Assert.AreEqual(exp, _rendererManager.RendererCount);

            // Resume the loop and check that the enemies keep spawning.
            _gameLoop.ResumeLoop();
            Thread.Sleep(1000);
            Assert.IsTrue(exp < _rendererManager.RendererCount);
        }

        [TestMethod]
        public void StopTest()
        {
            _gameLoop.StartLoop();
            _cts.Cancel();
            Assert.IsFalse(_gameLoop.IsAlive());
        }
    }
}
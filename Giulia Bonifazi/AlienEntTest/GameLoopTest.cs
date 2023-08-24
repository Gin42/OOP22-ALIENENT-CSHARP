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
        private readonly IGameLoop _gameLoop;
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
            _gameLoop.StartLoop();
            Thread.Sleep(1000);
            int exp = _rendererManager.RendererCount;
            _gameLoop.PauseLoop();
            Thread.Sleep(2000);
            Assert.AreEqual(exp, _rendererManager.RendererCount);

            _gameLoop.ResumeLoop();
            Thread.Sleep(1000);
            Assert.IsTrue(exp < _rendererManager.RendererCount);
        }
    }
}
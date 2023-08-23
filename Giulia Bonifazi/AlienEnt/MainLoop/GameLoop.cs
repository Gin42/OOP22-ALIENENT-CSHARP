using AlienEnt.Commons;
using AlienEnt.GameWorld;
using AlienEnt.Props;

namespace AlienEnt.MainLoop
{
    public sealed class GameLoop : GameLoopThread, IGameLoop
    {
        private static readonly double s_msPerFrame = 20;
        private readonly IWorld _world;
        private readonly PropRendererManager _rendererManager;
        private readonly PropEnemySpawner _enemySpawner;
        private bool _stopped;
        private bool _paused;
        
        public GameLoop(PropRendererManager rendererManager, IWorld world, string playerId)
            : base()
        {
            _world = world;
            _rendererManager = rendererManager;

            // Player and spawner setup.
            var player = new PropPlayerSpawner(world).GetPlayer(playerId);
            _world.Player = player;
            Point2D topRight = new(_world.Dimensions.Width,0);
            Point2D bottomLeft = new(0, _world.Dimensions.Height);
            _enemySpawner = new PropEnemySpawner(topRight, bottomLeft, _world, player);
            _stopped = false;
            _paused = false;
        }

        public void PauseLoop()
        {
            _paused = true;
        }


        public void ResumeLoop()
        {
            _paused = false;
            Monitor.PulseAll(this);
        }

        public override void RunThread()
        {
            long previousStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            while (!_stopped)
            {
                if (_paused)
                {
                    lock (this)
                    {
                        while (_paused)
                        {
                            Monitor.Wait(this);
                        }
                    }
                    previousStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                }
                long currentStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long elapsed = currentStart - previousStart;
                ProcessInput();
                UpdateGame(elapsed / 1000);
                Render();
                WaitForNextFrame(DateTimeOffset.Now.ToUnixTimeMilliseconds() - currentStart);
                previousStart = currentStart;
            }
        }

        public void StopLoop()
        {
            ResumeLoop();
            _stopped = true;
            Stop();
        }

        private void Render()
        {
            _rendererManager.Render();
        }

        private void UpdateGame(double deltaTime)
        {
            _enemySpawner.Update(deltaTime);
            _world.Update(deltaTime);
            foreach(var o in _world.LastAdded)
            {
                _rendererManager.AddRenderer(o);
            }
        }

        private void WaitForNextFrame(double delta)
        {
            if (delta < s_msPerFrame)
            {
                Thread.Sleep((int) (s_msPerFrame - delta));
            }
        }

        private void ProcessInput()
        {
            
        }
    }
}
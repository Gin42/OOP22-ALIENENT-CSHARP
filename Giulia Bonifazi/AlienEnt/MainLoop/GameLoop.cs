using AlienEnt.Commons;
using AlienEnt.Commons.Queue;
using AlienEnt.GameWorld;
using AlienEnt.Props;

namespace AlienEnt.MainLoop
{
    /// <summary>
    /// GameLoop contains the GameLoopThread and implements the interface IGameLoop.
    /// </summary>
    public sealed class GameLoop : GameLoopThread, IGameLoop
    {
        private static readonly double s_msPerFrame = 20;
        private readonly CancellationToken _token;
        private readonly IWorld _world;
        private readonly PropRendererManager _rendererManager;
        private readonly PropEnemySpawner _enemySpawner;
        private readonly PropInputSupplier? _inputSupplier;
        private readonly InputQueue _inputQueue;
        private bool _stopped;
        private bool _paused;
        private bool _started;
        
        public GameLoop(InputQueue inputQueue, PropRendererManager rendererManager, IWorld world, string playerId,
            CancellationToken token) : base()
        {
            _world = world;
            _rendererManager = rendererManager;
            _inputQueue = inputQueue;
            _stopped = false;
            _paused = false;
            _started = false;
            _token = token;

            // Player and spawner setup.
            var player = new PropPlayerSpawner(world).GetPlayer(playerId);
            _world.Player = player;
            _inputSupplier = player.InputSupplier;
            Point2D topRight = new(_world.Dimensions.Width,0);
            Point2D bottomLeft = new(0, _world.Dimensions.Height);
            _enemySpawner = new PropEnemySpawner(topRight, bottomLeft, _world, player);
        }

        public void PauseLoop()
        {
            _paused = true;
        }


        public void ResumeLoop()
        {
            _paused = false;
        }

        public void StartLoop()
        {
            if (!_started)
            {
                _started = true;
                Start();
            }
        }

        public override void RunThread()
        {
            long previousStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            while (!_stopped)
            {
                if (_token.IsCancellationRequested)
                {
                    return;
                }
                long currentStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                if (!_paused)
                {
                    long elapsed = currentStart - previousStart;
                    ProcessInput();
                    UpdateGame(elapsed / 1000.0);
                    Render();
                }
                WaitForNextFrame(DateTimeOffset.Now.ToUnixTimeMilliseconds() - currentStart);
                previousStart = currentStart;

            }
        }

        public void StopLoop()
        {
            ResumeLoop();
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
            while (_inputQueue.Count > 0)
            {
                _inputSupplier?.AddInput(_inputQueue.TakeInput());
            }
        }
    }
}
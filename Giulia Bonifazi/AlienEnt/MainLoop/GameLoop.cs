using AlienEnt.GameWorld;
using AlienEnt.Props;

namespace AlienEnt.MainLoop
{
    public sealed class GameLoop : GameLoopThread, IGameLoop
    {
        private static readonly double s_msPerFrame = 20;
        private readonly IWorld _world;
        private readonly PropRendererManager _rendererManager;
        private bool _stopped;
        private bool _paused;
        
        public GameLoop(PropRendererManager rendererManager, IWorld world, string playerId)
            : base()
        {
            _world = world;
            _rendererManager = rendererManager;

            var player = new PropPlayerSpawner(world).GetPlayer(playerId);
            _world.AddGameObject(player);
        }

        public void PauseLoop()
        {
            
        }

        public override void PauseThread()
        {
            throw new NotImplementedException();
        }

        public void ResumeLoop()
        {
            
        }

        public override void RunThread()
        {
            throw new NotImplementedException();
        }

        public void StopLoop()
        {
            throw new NotImplementedException();
        }
    }
}
namespace AlienEnt.MainLoop
{
    /// <summary>
    /// This class is needed to incapsulate a Thread instance,
    /// as Thread itself is a sealed class, and thus cannot be extended.
    /// </summary>
    public abstract class GameLoopThread
    {
        private readonly Thread _thread;

        public GameLoopThread()
        {
            _thread = new Thread(new ThreadStart(RunThread));
        }

        public void Stop() => _thread.Join();

        public void Start() => _thread.Start();

        public bool IsAlive() => _thread.IsAlive;

        public abstract void RunThread();
    }
}
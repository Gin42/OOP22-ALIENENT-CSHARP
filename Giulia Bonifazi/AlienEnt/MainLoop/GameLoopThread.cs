namespace AlienEnt.MainLoop
{
    public abstract class GameLoopThread
    {
        private readonly Thread _thread;

        public GameLoopThread()
        {
            _thread = new Thread(new ThreadStart(RunThread));
        }

        public void Start() => _thread.Start();
        
        public void Stop() => _thread.Join();

        public abstract void RunThread();

        public abstract void PauseThread();
    }
}
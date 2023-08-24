namespace AlienEnt.MainLoop
{
    public abstract class GameLoopThread
    {
        private readonly Thread _thread;
        private readonly CancellationTokenSource _cts = new();

        public GameLoopThread()
        {
            _thread = new Thread(new ThreadStart(RunThread));
        }

        public void Start() => _thread.Start();
        
        public void Stop()
        {
            _cts.Cancel();
        }

        public abstract void RunThread();
    }
}
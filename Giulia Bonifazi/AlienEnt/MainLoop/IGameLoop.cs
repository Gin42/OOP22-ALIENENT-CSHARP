namespace AlienEnt.MainLoop 
{
    public interface IGameLoop
    {
        void StartLoop();

        void StopLoop();

        void PauseLoop();

        void ResumeLoop();
    }
}
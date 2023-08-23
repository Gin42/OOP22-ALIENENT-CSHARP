namespace AlienEnt.MainLoop 
{
    public interface IGameLoop
    {
        void StopLoop();

        void PauseLoop();

        void ResumeLoop();
    }
}
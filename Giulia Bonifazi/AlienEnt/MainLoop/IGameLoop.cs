namespace AlienEnt.MainLoop 
{
    /// <summary>
    /// GameLoop interface. It was necessary to add a start method because the 
    /// Thread class is sealed, so GameLoop cannot inherit from it and relies on its interface
    /// to make this method usable by other classes. The stop method was removed because
    /// now the thread is stopped with a CancellationToken.
    /// </summary>
    public interface IGameLoop
    {
        void StartLoop();

        void PauseLoop();

        void ResumeLoop();
    }
}
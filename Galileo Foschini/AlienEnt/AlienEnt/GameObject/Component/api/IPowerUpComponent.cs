namespace AlienEnt.GameObject.Component
{
    /// <summary>
    /// The component that have the job of updating the stats of a GameObject.
    /// </summary>
    public interface IPowerUpComponent : IComponent
    {
        /// <summary>
        /// Set the modifiers that will be applied to the statistics of the GameObject.
        /// </summary>
        /// <param name="powerUps"></param>
        void SetPowerUps(Dictionary<Statistic, int> powerUps);
    }
}
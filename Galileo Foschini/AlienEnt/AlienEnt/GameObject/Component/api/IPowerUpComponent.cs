namespace AlienEnt.GameObject.Component.Api
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
        void SetPowerUps(IDictionary<Statistic, int> powerUps);
    }
}
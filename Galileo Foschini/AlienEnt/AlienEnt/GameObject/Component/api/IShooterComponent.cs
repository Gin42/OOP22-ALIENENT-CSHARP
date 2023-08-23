namespace AlienEnt.GameObject.Component.Api
{
    public interface IShooterComponent : IComponent
    {
        /// <summary>
        /// The Source that creates the projectiles
        /// </summary>
        Func<IGameObject> ProjectileSupplier { get; set; }

        /// <summary>
        /// Tell the component that it should shoot
        /// Not all ShooterComponents utilize this method
        /// </summary>
        void Shoot();
    }
}
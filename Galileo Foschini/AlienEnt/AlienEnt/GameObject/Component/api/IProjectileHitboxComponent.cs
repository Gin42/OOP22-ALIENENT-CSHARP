namespace AlienEnt.GameObject.Component
{
    public interface IProjectileHitboxComponent : IHitboxComponent
    {
        GameObjectType ShooterType { get; set;}
    }
}
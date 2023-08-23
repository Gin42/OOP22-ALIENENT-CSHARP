namespace AlienEnt.GameObject.Component.Api
{
    public interface IProjectileHitboxComponent : IHitboxComponent
    {
        GameObjectType ShooterType { get; set; }
    }
}
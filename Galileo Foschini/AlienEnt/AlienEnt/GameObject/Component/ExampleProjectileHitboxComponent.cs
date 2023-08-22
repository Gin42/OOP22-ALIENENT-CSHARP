namespace AlienEnt.GameObject.Component
{
    public class ExampleProjectileHitboxComponent : ExampleHitboxComponent, IProjectileHitboxComponent
    {
        public ExampleProjectileHitboxComponent(IGameObject gameObject) : base(gameObject, IHitboxComponent.GameObjectType.PROJECTILE)
        {
        }

        public IHitboxComponent.GameObjectType ShooterType { get; set; }

        public override IComponent? Duplicate(IGameObject obj)
        {
            throw new NotImplementedException();
        }
    }
}
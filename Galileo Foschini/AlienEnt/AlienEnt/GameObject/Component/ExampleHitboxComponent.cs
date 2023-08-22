namespace AlienEnt.GameObject.Component
{
    public class ExampleHitboxComponent : AbstractComponent, IHitboxComponent
    {
        private readonly IHitboxComponent.GameObjectType _type;
        public ExampleHitboxComponent(IGameObject gameObject, IHitboxComponent.GameObjectType type) : base(gameObject, true)
        {
            _type = type;
        }

        public IHitboxComponent.GameObjectType ObjectType => _type;

        public override IComponent? Duplicate(IGameObject obj)
        {
            return new ExampleHitboxComponent(obj, ObjectType);
        }
    }
}
using Alienent.geometry;
using static Alienent.Api.IHitboxComponent;

namespace Alienent.Api
{

    public abstract class AbstractCircleHitboxComponent : AbstractComponent, IHitboxComponent
    {
        private readonly TypeObject _objectType;
        private Circle2D _hitbox;
        private readonly double _radius;

        public AbstractCircleHitboxComponent(IGameObject obj, bool enabled, TypeObject objectType, double radius)
            : base(obj, enabled)
        {
            _objectType = objectType;
            _radius = radius;
            _hitbox = new Circle2D(obj.GetPosition(), radius);
        }

        public Circle2D GetHitbox() => _hitbox;

        public void SetPosition() => _hitbox = new Circle2D(GetGameObject().GetPosition(), _radius);

        public override void Update(double deltaTime) => SetPosition();

        public override void Start() => SetPosition();

        public TypeObject GetTypeObject() => _objectType;

        public void CanCollide(IHitboxComponent hitbox)
        {
            if (_objectType != hitbox.GetTypeObject())
            {
                if (hitbox is AbstractCircleHitboxComponent circleHitbox && _hitbox.IntersectWith(circleHitbox.GetHitbox()))
                {
                    IsColliding(hitbox);
                    hitbox.IsColliding(this);
                }
                else if (hitbox is BoundaryHitboxComponent component && _hitbox.IntersectWith(component.GetLine()))
                {
                    hitbox.IsColliding(this);
                }
            }
        }

        public abstract void IsColliding(IHitboxComponent hitbox);

        public override abstract IComponent? Duplicate(IGameObject obj);
    }
}
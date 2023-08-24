using Alienent.Api;
using Alienent.geometry;
using static Alienent.IBoundaryHitboxComponent;
using static Alienent.Api.IHitboxComponent;

namespace Alienent
{
    public class BoundaryHitboxComponent : AbstractComponent, IBoundaryHitboxComponent
    {
        private const int DESTROY = 999;
        private readonly TypeObject _objectType;
        private readonly Line2D _line;
        private Locations _location;

        public BoundaryHitboxComponent(IGameObject obj, bool enabled, TypeObject objectType, Point2D p1, Point2D p2)
            : base(obj, enabled)
        {
            _objectType = objectType;
            _line = Line2D.FromTwoPoints(p1, p2);
        }

        public void IsColliding(IHitboxComponent hitbox)
        {
            if (hitbox.GetTypeObject() == TypeObject.PROJECTILE)
            {
                hitbox.GetGameObject().Hit(DESTROY);
            }
            else if (_location == Locations.UP)
            {
                    hitbox.GetGameObject().SetPosition(Vector2D.FromComponents(0,
                    ((AbstractCircleHitboxComponent)hitbox).GetHitbox().GetRay() - _line.DistancePoint(hitbox.GetGameObject().GetPosition()))
                    .Translate(hitbox.GetGameObject().GetPosition()));
            }
            else if (_location == Locations.DOWN)
            {
                hitbox.GetGameObject().SetPosition(Vector2D.FromComponents(0,
                    -(((AbstractCircleHitboxComponent)hitbox).GetHitbox().GetRay() - _line.DistancePoint(hitbox.GetGameObject().GetPosition())))
                    .Translate(hitbox.GetGameObject().GetPosition()));
            }
            else if (_location == Locations.RIGHT)
            {
                hitbox.GetGameObject().SetPosition(Vector2D.FromComponents(
                    -(((AbstractCircleHitboxComponent)hitbox).GetHitbox().GetRay() - _line.DistancePoint(hitbox.GetGameObject().GetPosition())), 0)
                    .Translate(hitbox.GetGameObject().GetPosition()));
            }
            else if (_location == Locations.LEFT)
            {
                hitbox.GetGameObject().SetPosition(Vector2D.FromComponents(
                    ((AbstractCircleHitboxComponent)hitbox).GetHitbox().GetRay() - _line.DistancePoint(hitbox.GetGameObject().GetPosition()), 0)
                    .Translate(hitbox.GetGameObject().GetPosition()));
            }
        }

        public void SetLocations(Locations location)
        {
            _location = location;
        }

        public TypeObject GetTypeObject() => _objectType;

        public void CanCollide(IHitboxComponent hitbox)
        {
            if (_objectType != hitbox.GetTypeObject() && hitbox is AbstractCircleHitboxComponent component &&
                component.GetHitbox().IntersectWith(_line))
            {
                IsColliding(hitbox);
            }
        }

        public Line2D GetLine() => _line;

        public override IComponent? Duplicate(IGameObject obj) => null;
    }
}
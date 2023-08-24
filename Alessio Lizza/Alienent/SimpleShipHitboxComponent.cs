using Alienent.Api;
using static Alienent.Api.IHitboxComponent;

namespace Alienent
{
    public class SimpleShipHitboxComponent : AbstractCircleHitboxComponent
    {
        public SimpleShipHitboxComponent(IGameObject @object, bool enabled, TypeObject objectType, double radius)
            : base(@object, enabled, objectType, radius)
        {
        }

        public override void IsColliding(IHitboxComponent hitbox)
        {
            if (hitbox is IProjectileHitboxComponent component && component.GetShooter() != GetTypeObject())
            {
                hitbox.GetGameObject().Hit(1);
            }
        }

        public override IComponent? Duplicate(IGameObject obj) => new SimpleShipHitboxComponent(obj, IsEnabled(), GetTypeObject(), GetHitbox().GetRay());
    }

}
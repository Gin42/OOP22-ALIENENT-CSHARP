using Alienent.Api;
using static Alienent.Api.IHitboxComponent;

namespace Alienent
{

    public class BomberHitboxComponentImpl : AbstractCircleHitboxComponent
    {
        private const int AUTOKILLDAMAGE = 99;

        public BomberHitboxComponentImpl(IGameObject obj, bool enabled, TypeObject objectType, double radius)
            : base(obj, enabled, objectType, radius)
        {
        }

        public override void IsColliding(IHitboxComponent hitbox)
        {
            if (hitbox.GetTypeObject() == TypeObject.PLAYER)
            {
                hitbox.GetGameObject().Hit(GetGameObject().GetStatValue(Statistic.DAMAGE));
                GetGameObject().Hit(AUTOKILLDAMAGE);
            }
            if (hitbox is SimpleProjectileHitboxComponent component && component.GetShooter() == TypeObject.PLAYER)
            {
                hitbox.GetGameObject().Hit(1);
            }
        }

        public override IComponent? Duplicate(IGameObject obj) => new BomberHitboxComponentImpl(obj, IsEnabled(), TypeObject.ENEMY, GetHitbox().GetRay());
    }

}
using static Alienent.IHitboxComponent;

namespace Alienent {
    public class SimpleProjectileHitboxComponent : AbstractCircleHitboxComponent, IProjectileHitboxComponent
    {
        private TypeObject shooter;

        public SimpleProjectileHitboxComponent(IGameObject @object, bool enabled, TypeObject objectType, int radius)
            : base(@object, enabled, objectType, radius)
        {
        }

        public override void IsColliding(IHitboxComponent hitbox)
        {
            if (hitbox.GetTypeObject() != TypeObject.PROJECTILE && hitbox.GetTypeObject() != shooter)
            {
                hitbox.GetGameObject().Hit(base.GetGameObject().GetStatValue(Statistic.DAMAGE));
            }
        }

        public TypeObject GetShooter() => shooter;

        public void SetShooter(TypeObject type) => shooter = type;

        public override IComponent? Duplicate(IGameObject obj) => null;
    }
}
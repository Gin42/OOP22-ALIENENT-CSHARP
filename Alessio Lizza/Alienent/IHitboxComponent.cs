namespace Alienent{
    public interface IHitboxComponent : IComponent
    {
        enum TypeObject
        {
            ENEMY,
            PLAYER,
            PROJECTILE,
            BOUNDARY
        }

        TypeObject GetTypeObject();

        void CanCollide(IHitboxComponent hitbox);

        void IsColliding(IHitboxComponent hitbox);

    }
}
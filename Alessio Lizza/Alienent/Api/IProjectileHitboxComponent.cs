namespace Alienent.Api
{
    public interface IProjectileHitboxComponent : IHitboxComponent
    {
        TypeObject GetShooter();
        void SetShooter(TypeObject type);
    }

}
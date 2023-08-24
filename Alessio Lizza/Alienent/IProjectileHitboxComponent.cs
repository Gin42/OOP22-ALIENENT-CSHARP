namespace Alienent {
    public interface IProjectileHitboxComponent : IHitboxComponent
{
    TypeObject GetShooter();
    void SetShooter(TypeObject type);
}

}
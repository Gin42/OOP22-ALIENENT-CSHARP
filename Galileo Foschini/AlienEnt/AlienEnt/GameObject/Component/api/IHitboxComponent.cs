namespace AlienEnt.GameObject.Component
{
    public interface IHitboxComponent : IComponent
    {
        public enum GameObjectType
        {
            PLAYER,
            ENEMY,
            PROJECTILE,
            BOUNDARY
        }

        GameObjectType ObjectType { get; }
    }
}
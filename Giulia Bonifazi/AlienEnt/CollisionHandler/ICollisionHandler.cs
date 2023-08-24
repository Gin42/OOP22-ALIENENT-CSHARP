using AlienEnt.Props;

namespace AlienEnt.CollisionHandler {
    /// <summary>
    /// CollisionHandler interface.
    /// </summary>
    public interface ICollisionHandler {

        void CheckCollisions();

        void AddHitbox(PropHitbox? toAdd);

        void RemoveHitbox(PropHitbox? toRemove);
    }
}
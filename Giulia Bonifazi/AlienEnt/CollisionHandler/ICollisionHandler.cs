using AlienEnt.Props;

namespace AlienEnt.CollisionHandler {
    public interface ICollisionHandler {

        void CheckCollisions();

        void AddHitbox(PropHitbox? toAdd);

        void RemoveHitbox(PropHitbox? toRemove);
    }
}
using Props;

namespace CollisionHandler {
    public interface ICollisionHandler {

        void CheckCollisions();

        void AddHitbox(PropHitbox? toAdd);

        void RemoveHitbox(PropHitbox? toRemove);
    }
}
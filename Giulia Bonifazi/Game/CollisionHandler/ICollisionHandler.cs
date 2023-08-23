using Props;

namespace CollisionHandler {
    public interface ICollisionHandler {

        void checkCollisions();

        void addHitbox(PropHitbox toAdd);

        void removeHitbox(PropHitbox toRemove);
    }
}
using Props;

namespace CollisionHandler {
    public class SimpleCollisionHandler : CollisionHandler
    {
        protected override void CheckPair(PropHitbox a, PropHitbox b)
        {
            a.Collide(b);
            b.Collide(a);
        }
    }
}
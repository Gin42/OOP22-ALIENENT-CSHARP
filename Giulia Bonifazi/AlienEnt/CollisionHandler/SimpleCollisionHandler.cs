using AlienEnt.Props;

namespace AlienEnt.CollisionHandler {
    public class SimpleCollisionHandler : AbstractCollisionHandler
    {
        protected override void CheckPair(PropHitbox a, PropHitbox b)
        {
            a.Collide(b);
            b.Collide(a);
        }
    }
}
using AlienEnt.Props;

namespace AlienEnt.CollisionHandler {
    /// <summary>
    /// This class implements the simplest possible version of the CheckPair method.
    /// </summary>
    public sealed class SimpleCollisionHandler : AbstractCollisionHandler
    {
        protected override void CheckPair(PropHitbox a, PropHitbox b)
        {
            a.Collide(b);
            b.Collide(a);
        }
    }
}
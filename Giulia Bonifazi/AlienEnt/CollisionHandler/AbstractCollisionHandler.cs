using AlienEnt.Props;

namespace AlienEnt.CollisionHandler {
    /// <summary>
    /// This abstract class allows us to carry over the recursive check method to other
    /// implementations.
    /// </summary>
    public abstract class AbstractCollisionHandler : ICollisionHandler 
    {
        private readonly IList<PropHitbox> _collidables;

        public AbstractCollisionHandler () 
        {
            _collidables = new List<PropHitbox>(); 
        }

        public void AddHitbox(PropHitbox? toAdd)
        {
            if(toAdd != null) {
                _collidables.Add(toAdd);
            }
        }

        public void CheckCollisions()
        {
            RecCheck(_collidables, 0);
        }

        public void RemoveHitbox(PropHitbox? toRemove)
        {
            if(toRemove != null) {
                _collidables.Remove(toRemove);
            }
        }

        protected abstract void CheckPair(PropHitbox a, PropHitbox b);

        private void RecCheck(IList<PropHitbox> list, int num) 
        {
            if (num > list.Count - 1)
            {
                return;
            }
            else
            {
                var first = list.AsEnumerable().Skip(num).First();
                list.AsEnumerable().Skip(num + 1).ToList().ForEach(e => CheckPair(e, first));
                RecCheck(list , num + 1);
            }
        }
    }
}
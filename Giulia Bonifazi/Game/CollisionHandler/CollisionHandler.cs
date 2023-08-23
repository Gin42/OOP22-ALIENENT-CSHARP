using Props;

namespace CollisionHandler {
    public abstract class CollisionHandler : ICollisionHandler 
    {
        private readonly IList<PropHitbox> _collidables;

        public CollisionHandler () 
        {
            _collidables = new List<PropHitbox>(); 
        }

        public void addHitbox(PropHitbox toAdd)
        {
            if(toAdd != null) {
                _collidables.Add(toAdd);
            }
        }

        public void checkCollisions()
        {
            RecCheck(_collidables, 0);
        }

        public void removeHitbox(PropHitbox? toRemove)
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
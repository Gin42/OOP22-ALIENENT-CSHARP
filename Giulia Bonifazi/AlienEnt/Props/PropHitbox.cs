using AlienEnt.Commons;

namespace AlienEnt.Props 
{
    public class PropHitbox 
    {
        private readonly PropGameObject _gameObject;

        public PropHitbox (PropGameObject obj, Point2D mov) 
        {
            _gameObject = obj;
            Mov = mov;
            HasCollided = false;
        }
        
        public Point2D Mov {get; set;}

        public bool HasCollided {get; private set;}
        
        public void Update() 
        {
            _gameObject.Pos.X += Mov.X;
            _gameObject.Pos.Y += Mov.Y;
        }

        public void Collide(PropHitbox hb) 
        {
            if (hb.GetPos().IsSame(_gameObject.Pos)) {
                HasCollided = true;
                _gameObject.Hp = 0;
            }
        }

        protected Point2D GetPos()
        {
            return _gameObject.Pos;
        }
    }
}
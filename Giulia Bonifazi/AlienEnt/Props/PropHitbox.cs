using AlienEnt.Commons;

namespace AlienEnt.Props {
    public class PropHitbox {
        private readonly PropGameObject _gameObject;
        public Point2D Mov {get; set;}

        public bool HasCollided {get; private set;}

        public PropHitbox (PropGameObject obj, Point2D mov) 
        {
            _gameObject = obj;
            Mov = mov;
            HasCollided = false;
        }
        
        public void Update() 
        {
            _gameObject.Pos.X += Mov.X;
            _gameObject.Pos.Y += Mov.Y;
            if (HasCollided)
            {
                _gameObject.Hp = 0;
            }
        }

        public void Collide(PropHitbox hb) 
        {
            if (hb.GetPos().Equals(_gameObject.Pos)) {
                HasCollided = true;
            }
        }

        public Point2D GetPos()
        {
            return _gameObject.Pos;
        }
    }
}
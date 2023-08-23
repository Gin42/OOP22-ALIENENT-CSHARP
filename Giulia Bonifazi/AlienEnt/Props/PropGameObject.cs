using AlienEnt.Commons;

namespace AlienEnt.Props{
    public class PropGameObject { 

        public bool IsAlive { 
            get => Hp > 0; 
            }
        public Point2D Pos { get; set; }
        public int Hp { get; set; }
        public PropHitbox? Hitbox { get; set; }
        public string Tag { get; private set; }

        public PropGameObject (Point2D pos, Dictionary<PropStatistic,int> stats, string tag) {
            Pos = pos;
            Hp = stats[PropStatistic.HP];
            Tag = tag;
        }
        
        public void Update(double deltaTime) {
            Hitbox?.Update();
        }
    }

}
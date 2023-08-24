using AlienEnt.Commons;

namespace AlienEnt.Props
{
    public class PropGameObject 
    { 


        public PropGameObject (Point2D pos, Dictionary<PropStatistic,int> stats, string tag) 
        {
            Pos = pos;
            Hp = stats[PropStatistic.HP];
            Tag = tag;
        }

        public bool IsAlive 
        { 
            get => Hp > 0;    
        }
        public Point2D Pos { get; set; }
        public int Hp { get; set; }
        public PropHitbox? Hitbox { get; set; }
        public PropInputSupplier? InputSupplier{ get; set; }
        public string Tag { get; private set; }
        
        public void Update(double deltaTime) 
        {
            Hitbox?.Update();
        }
    }

}
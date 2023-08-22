using System.Runtime.CompilerServices;
using AlienEnt.Geometry;
using GameObject.Component;

namespace GameObject
{
    public class GameObject : IGameObject
    {

        private readonly Dictionary<Statistic, int> _stats;
        private readonly List<IComponent> _components; 

        private int _hp;
        private double _recoveryCooldown = 0;

        public GameObject(Point2D position, Vector2D velocity, IDictionary<Statistic, int> stats, string id)
        {
            Position = position;
            Velocity = velocity;
            _stats = new Dictionary<Statistic, int>(stats);
            Id = id;
            _components = new List<IComponent>();
            if(!_stats.TryGetValue(Statistic.HP, out _hp))
                _hp = 0;
        }

        public Point2D Position { get; set; }
        public Vector2D Velocity { get; set; }
        public string Id { get;}

        public void AddAllComponents(ICollection<IComponent> components)
        {
            _components.AddRange(components);
        }

        public void AddComponent(IComponent component)
        {
            _components.Add(component);
        }

        public List<IComponent> GetAllComponents()
        {
            return new List<IComponent>(_components);
        }

        public Dictionary<Statistic, int> GetAllStats()
        {
            return new Dictionary<Statistic, int>(_stats);
        }

        public int GetHealth()
        {
            return _hp;
        }

        public int? GetStatValue(Statistic stat)
        {
            _stats.TryGetValue(stat, out int value);
            return value;
        }

        public void Heal(int heal)
        {
            if (heal < 0)
                throw new ArgumentOutOfRangeException("the healing value must be positive");
            _hp += heal;
        }

        public void Hit(int damage)
        {
            if(damage < 0)
                throw new ArgumentOutOfRangeException("the damage value must be positive");
            _hp -= damage;
        }

        public bool IsAlive()
        {
            return _hp > 0;
        }

        public void Recovery(double deltatime)
        {
            if(_recoveryCooldown > 1)
            {
                Heal(GetStatValue(Statistic.RECOVERY) ?? 0);
                _recoveryCooldown = 0;
            } else
                _recoveryCooldown += deltatime;
        }

        public void SetStatValue(Statistic stat, int value)
        {
            if (!_stats.TryAdd(stat, value))
                _stats[stat]=value;
        }

        public void Update(double deltaTime)
        {
            foreach (var component in _components)
                component.Update(deltaTime);
            Recovery(deltaTime);
        }
    }
}
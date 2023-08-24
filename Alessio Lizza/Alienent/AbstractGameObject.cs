namespace Alienent{
    public class GameObjectAbs : IGameObject
    {
        private Point2D _position;
        private Vector2D _velocity;
        private int _health;
        private readonly IDictionary<Statistic, int> _stats;
        private readonly ISet<IComponent> _component;
        private readonly string _id;
        private double _recoveryCooldown;

        public GameObjectAbs(Point2D pos, Vector2D vector, Dictionary<Statistic, int> stat, string id)
        {
            _position = pos;
            _velocity = vector;
            _stats = new Dictionary<Statistic, int>(stat);
            _health = _stats[Statistic.HP];
            _id = id;
            _component = new HashSet<IComponent>();
        }

        public bool IsAlive() => GetHealth() > 0;

        public Point2D GetPosition() => _position;

        public void SetPosition(Point2D point) => _position = point;

        public Vector2D Velocity => _velocity;

        public void SetVelocity(Vector2D vector) => _velocity = vector;

        public List<IComponent> GetAllComponent() => new(_component);

        public int GetStatValue(Statistic stat) => _stats[stat];

        public IDictionary<Statistic, int> GetAllStats() => new Dictionary<Statistic, int>(_stats);

        public void SetStatValue(Statistic stat, int value) => _stats[stat] = value;

        public void AddComponent(IComponent comp) => _component.Add(comp);

        public void Hit(int dmg) => _health -= dmg;

        public void Heal(int heal)
        {
            _health += heal;
            if (_health > _stats[Statistic.HP])
            {
                _health = _stats[Statistic.HP];
            }
        }

        public void Update(double deltaTime)
        {
            foreach (var comp in GetAllComponent())
            {
                comp.Update(deltaTime);
            }
            Recovery(deltaTime);
        }

        public int GetHealth() => _health;

        public void Recovery(double deltaTime)
        {
            if (_recoveryCooldown > 1)
            {
                Heal(_stats.ContainsKey(Statistic.RECOVERY) ? _stats[Statistic.RECOVERY] : 0);
                _recoveryCooldown = 0;
            }
            else
            {
                _recoveryCooldown += deltaTime;
            }
        }

        public string GetId() => _id;

        public void AddAllComponent(IEnumerable<IComponent> components) => _component.UnionWith(components);

        public T? GetComponent<T>() where T : IComponent => GetAllComponent().OfType<T>().FirstOrDefault();
    }
}
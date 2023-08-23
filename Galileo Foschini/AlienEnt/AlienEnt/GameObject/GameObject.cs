using AlienEnt.Geometry;
using AlienEnt.GameObject.Component.Api;

namespace AlienEnt.GameObject
{
    public class GameObject : IGameObject
    {

        private readonly Dictionary<Statistic, int> _stats;
        private readonly ISet<IComponent> _components;

        private int _hp;
        private double _recoveryCooldown = 0;

        /// <summary>
        /// Contructor for all objects.
        /// </summary>
        /// <param name="position">Point2D of the position of the object.</param>
        /// <param name="velocity">Vector2D of the velocity of the object</param>
        /// <param name="stats">a dictionary with all the basic statistics of the object.</param>
        /// <param name="id">the string id of the object.</param>
        public GameObject(Point2D position, Vector2D velocity, IDictionary<Statistic, int> stats, string id)
        {
            Position = position;
            Velocity = velocity;
            _stats = new Dictionary<Statistic, int>(stats);
            Id = id;
            _components = new HashSet<IComponent>();
            if (!_stats.TryGetValue(Statistic.HP, out _hp))
                _hp = 0;
        }

        /// <inheritdoc/>
        public Point2D Position { get; set; }
        /// <inheritdoc/>
        public Vector2D Velocity { get; set; }
        /// <inheritdoc/>
        public string Id { get; }

        /// <inheritdoc/>
        public void AddAllComponents(ICollection<IComponent> components)
        {
            _components.UnionWith(components);
        }

        /// <inheritdoc/>
        public void AddComponent(IComponent component)
        {
            _components.Add(component);
        }

        /// <inheritdoc/>
        public ISet<IComponent> GetAllComponents()
        {
            return new HashSet<IComponent>(_components);
        }

        /// <inheritdoc/>
        public Dictionary<Statistic, int> GetAllStats()
        {
            return new Dictionary<Statistic, int>(_stats);
        }

        /// <inheritdoc/>
        public int GetHealth()
        {
            return _hp;
        }

        /// <inheritdoc/>
        public int? GetStatValue(Statistic stat)
        {
            _stats.TryGetValue(stat, out int value);
            return value;
        }

        /// <inheritdoc/>
        public void Heal(int heal)
        {
            if (heal < 0)
                throw new ArgumentOutOfRangeException("the healing value must be positive");
            _hp += heal;
        }

        /// <inheritdoc/>
        public void Hit(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException("the damage value must be positive");
            _hp -= damage;
        }

        /// <inheritdoc/>
        public bool IsAlive()
        {
            return _hp > 0;
        }

        /// <inheritdoc/>
        public void Recovery(double deltatime)
        {
            if (_recoveryCooldown > 1)
            {
                Heal(GetStatValue(Statistic.RECOVERY) ?? 0);
                _recoveryCooldown = 0;
            }
            else
                _recoveryCooldown += deltatime;
        }

        /// <inheritdoc/>
        public void SetStatValue(Statistic stat, int value)
        {
            if (!_stats.TryAdd(stat, value))
                _stats[stat] = value;
        }

        /// <inheritdoc/>
        public void Update(double deltaTime)
        {
            foreach (var component in _components)
                component.Update(deltaTime);
            Recovery(deltaTime);
        }
    }
}
using AlienEnt.Geometry;
using AlienEnt.GameObject.Component.Api;

namespace AlienEnt.GameObject
{
    /// <summary>
    /// Implements methods common to all objects.
    /// </summary>
    public class GameObject : IGameObject
    {

        private readonly IDictionary<Statistic, int> _stats;
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
        public ISet<IComponent> GetAllComponents() => new HashSet<IComponent>(_components);

        /// <inheritdoc/>
        public T? GetComponent<T>() where T : IComponent
        {
            return (T?) GetAllComponents()
                    .FirstOrDefault(com => com is T, null);
        }

        /// <inheritdoc/>
        public IDictionary<Statistic, int> GetAllStats() => new Dictionary<Statistic, int>(_stats);

        /// <inheritdoc/>
        public int GetHealth() => _hp;

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
                throw new ArgumentOutOfRangeException($"the value {heal} is not valid for the heal method");
            _hp += heal;
            if(_hp > GetStatValue(Statistic.HP))
                _hp = GetStatValue(Statistic.HP) ?? 0;

        }

        /// <inheritdoc/>
        public void Hit(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException($"the value {damage} is not valid for the hit method");
            _hp -= damage;
        }

        /// <inheritdoc/>
        public bool IsAlive() => _hp > 0;

        /// <inheritdoc/>
        public void Recovery(double deltatime)
        {
            _recoveryCooldown += deltatime;
            if (_recoveryCooldown > 1)
            {
                Heal(GetStatValue(Statistic.RECOVERY) ?? 0);
                _recoveryCooldown = 0;
            }
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
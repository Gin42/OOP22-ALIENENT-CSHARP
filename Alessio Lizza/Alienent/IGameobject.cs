namespace Alienent {
    public interface IGameObject
    {
        int GetStatValue(Statistic stat);

        bool IsAlive();

        Point2D GetPosition();

        Vector2D Velocity { get; }

        void SetPosition(Point2D point);

        void SetVelocity(Vector2D vector);

        T? GetComponent<T>() where T : IComponent;

        List<IComponent> GetAllComponent();

        IDictionary<Statistic, int> GetAllStats();

        void SetStatValue(Statistic stat, int value);

        void AddComponent(IComponent component);

        void Hit(int damage);

        void Heal(int heal);

        void AddAllComponent(IEnumerable<IComponent> components);

        int GetHealth();

        void Recovery(double deltaTime);

        string GetId();

        void Update(double deltaTime);
    }
}
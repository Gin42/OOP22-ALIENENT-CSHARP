namespace Alienent{
    public interface IComponent
    {
        void Update(double deltaTime);

        void Start();

        void Enable();

        void Disable();

        bool IsEnabled();

        IGameObject GetGameObject();

        IComponent? Duplicate(IGameObject obj);
    }
}
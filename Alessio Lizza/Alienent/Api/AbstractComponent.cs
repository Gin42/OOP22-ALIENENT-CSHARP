namespace Alienent.Api
{
    public abstract class AbstractComponent : IComponent
    {
        private readonly IGameObject _gameObject;
        private bool _enabled;

        public AbstractComponent(IGameObject obj, bool enabled)
        {
            _gameObject = obj;
            _enabled = enabled;
        }

        public virtual void Update(double deltaTime)
        {
            // This method is empty
        }

        public virtual void Start()
        {
            // This method is empty
        }

        public void Enable() => _enabled = true;

        public void Disable() => _enabled = false;

        public bool IsEnabled() => _enabled;

        public IGameObject GetGameObject() => _gameObject;

        public abstract IComponent? Duplicate(IGameObject obj);
    }

}
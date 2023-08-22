namespace AlienEnt.GameObject.Component
{
    public abstract class AbstractComponent : IComponent
    {
        private readonly IGameObject _gameObject;

        private bool _isEnabled;

        public AbstractComponent(IGameObject gameObject, bool IsEnabled)
        {
            _gameObject = gameObject;
            _isEnabled = IsEnabled;
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        public void Enable()
        {
            _isEnabled = true;
        }

        public bool IsEnabled()
        {
            return _isEnabled;
        }

        public IGameObject GetGameObject()
        {
            return _gameObject;
        }

        public virtual void Start()
        {
            // This method is empty on purpose,
            // so that not all Components must impement it
        }

        public virtual void Update(double deltaTime)
        {
            // This method is empty on purpose,
            // so that not all Components must impement it
        }

        public abstract IComponent? Duplicate(IGameObject obj);
    }
}
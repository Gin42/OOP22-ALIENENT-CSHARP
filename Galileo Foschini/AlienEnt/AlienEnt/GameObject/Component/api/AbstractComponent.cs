namespace AlienEnt.GameObject.Component.Api
{
    /// <summary>
    /// Basic implementation of Component
    /// </summary>
    public abstract class AbstractComponent : IComponent
    {
        private readonly IGameObject _gameObject;

        private bool _isEnabled;

        /// <summary>
        /// Set up the base for a component.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="IsEnabled">
        ///  the initial state of the component
        ///  ( true = enabled, false = disabled)
        ///  Not all components can be disabled
        /// </param>
        public AbstractComponent(IGameObject gameObject, bool IsEnabled)
        {
            _gameObject = gameObject;
            _isEnabled = IsEnabled;
        }

        /// <inheritdoc/>
        public void Disable()
        {
            _isEnabled = false;
        }

        /// <inheritdoc/>
        public void Enable()
        {
            _isEnabled = true;
        }

        /// <inheritdoc/>
        public bool IsEnabled()
        {
            return _isEnabled;
        }

        /// <inheritdoc/>
        public IGameObject GetGameObject()
        {
            return _gameObject;
        }

        /// <inheritdoc/>
        public virtual void Start()
        {
            // This method is empty on purpose,
            // so that not all Components must impement it
        }

        /// <inheritdoc/>
        public virtual void Update(double deltaTime)
        {
            // This method is empty on purpose,
            // so that not all Components must impement it
        }

        /// <inheritdoc/>
        public abstract IComponent? Duplicate(IGameObject obj);
    }
}
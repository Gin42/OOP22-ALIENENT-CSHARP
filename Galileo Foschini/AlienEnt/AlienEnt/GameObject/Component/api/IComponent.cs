namespace AlienEnt.GameObject.Component.Api
{
    public interface IComponent
    {
        /// <summary>
        /// The method that will be called every frame.
        /// Not all Compoments use a update method.
        /// </summary>
        /// <param name="deltaTime">the time passed between frames in seconds</param>
        void Update(double deltaTime);

        /// <summary>
        /// A method that should be called before using a component.
        /// Not all components need or use a start method.
        /// </summary>
        void Start();

        /// <summary>
        /// Set the component as enabled.
        /// Not all components can be enabled.
        /// </summary>
        void Enable();

        /// <summary>
        /// Set the component as disabled.
        /// Not all components can be disabled.
        /// </summary>
        void Disable();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the component is enabled.</returns>
        bool IsEnabled();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>the game object referenced by the component.</returns>
        IGameObject GetGameObject();

        /// <summary>
        /// Creates a copy of the component referening the given object.
        /// </summary>
        /// <param name="obj">The new object to be referenced</param>
        /// <returns>A copy of the component referencing the new GameObject, 
        ///         or null if the component cannot be duplicated or the operation failed.</returns>
        IComponent? Duplicate(IGameObject obj);
    }
}
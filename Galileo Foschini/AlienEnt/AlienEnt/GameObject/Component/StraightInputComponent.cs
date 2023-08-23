using AlienEnt.GameObject.Component.Api;

namespace AlienEnt.GameObject.Component
{
    /// <summary>
    /// An InputComponent that goes in a straight line.
    /// </summary>
    public class StraightInputComponent : AbstractComponent, IInputComponent
    {
        /// <summary>
        /// set Up a StraightInputComponent.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="IsEnabled"></param>
        public StraightInputComponent(IGameObject gameObject, bool IsEnabled) : base(gameObject, IsEnabled)
        {
        }

        /// <inheritdoc/>
        public override void Update(double deltaTime)
        {
            GetGameObject().Position = GetGameObject().Velocity.Mul(deltaTime).Translate(GetGameObject().Position);
        }

        /// <inheritdoc/>
        public override IComponent? Duplicate(IGameObject obj) => new StraightInputComponent(obj, IsEnabled());
    }
}
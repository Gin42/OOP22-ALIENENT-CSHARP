namespace AlienEnt.GameObject.Component.Api
{
    /// <summary>
    /// The input component for the player
    /// </summary>
    public interface IPlayerInputComponent : IInputComponent
    {
        /// <summary>
        /// The source of the inputs
        /// </summary>
        IInputSupplier InputSupplier { get; set; }

        /// <summary>
        /// The optional ShooterComponent
        /// </summary>
        IShooterComponent? ShooterComponent { get; }
    }
}
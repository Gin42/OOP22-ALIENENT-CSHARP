namespace AlienEnt.GameObject
{
    /// <summary>
    /// InputSupplier.
    /// </summary>
    public interface IInputSupplier
    {
        /// <summary>
        /// An enum that descibes the Inputs that can be given through an InputSupplier.
        /// </summary>
        enum Input
        {
            ACCELERATE,
            STOP_ACCELERATE,
            TURN_LEFT,
            TURN_RIGHT,
            SHOOT,
            NOTHING
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The Set of the inputs</returns>
        ISet<Input> GetInputs();

        /// <summary>
        /// Add the given input to the supplier
        /// </summary>
        /// <param name="input"></param>
        void AddInput(Input input);

        /// <summary>
        /// Remove all the previous inputs
        /// </summary>
        void ClearInputs();
    }
}
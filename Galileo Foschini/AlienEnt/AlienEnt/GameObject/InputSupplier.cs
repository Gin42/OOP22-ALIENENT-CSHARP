using static AlienEnt.GameObject.IInputSupplier;

namespace AlienEnt.GameObject
{
    /// <summary>
    /// InputSupplier standard.
    /// </summary>
    public class InputSupplier : IInputSupplier
    {
        private readonly ISet<Input> _inputs;

        /// <summary>
        /// 
        /// </summary>
        public InputSupplier()
        {
            _inputs = new HashSet<Input>();
        }

        /// <inheritdoc/>
        public void AddInput(Input input)
        {
            _inputs.Add(input);
        }

        /// <inheritdoc/>
        public void ClearInputs()
        {
            _inputs.Clear();
        }

        /// <inheritdoc/>
        public ISet<Input> GetInputs()
        {
            return new HashSet<Input>(_inputs);
        }
    }
}
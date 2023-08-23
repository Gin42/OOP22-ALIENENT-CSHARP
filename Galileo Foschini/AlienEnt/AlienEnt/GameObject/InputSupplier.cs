using static AlienEnt.GameObject.IInputSupplier;

namespace AlienEnt.GameObject
{
    public class InputSupplier : IInputSupplier
    {
        private readonly ISet<Input> _inputs;

        public InputSupplier()
        {
            _inputs = new HashSet<Input>();
        }

        public void AddInput(Input input)
        {
            _inputs.Add(input);
        }

        public void ClearInputs()
        {
            _inputs.Clear();
        }

        public ISet<Input> GetInputs()
        {
            return new HashSet<Input>(_inputs);
        }
    }
}
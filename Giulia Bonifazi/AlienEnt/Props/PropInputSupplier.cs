using System.ComponentModel;

namespace AlienEnt.Props
{
    public class PropInputSupplier
    {
        [DefaultValue(0)]
        public int Added { get; private set; }
        
        public void AddInput(PropInput input)
        {
            Added++;
        }
    }
}
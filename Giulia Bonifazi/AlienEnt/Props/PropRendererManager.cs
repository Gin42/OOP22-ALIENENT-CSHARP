using System.ComponentModel;

namespace AlienEnt.Props
{
    public class PropRendererManager
    {
        [DefaultValue(0)]
        public int RendererCount { get; private set; }

        public void AddRenderer(PropGameObject obj){
            RendererCount++;
        }
    }
}
using System.Collections.Concurrent;
using AlienEnt.Props;

namespace AlienEnt.Commons.Queue 
{
    /// <summary>
    /// This class is a bridge that allows the inputs captured by the view to
    /// reach the model and move the player character. 
    /// </summary>

    public class InputQueue : BlockingCollection<string>
    {

        public InputQueue(int maxSize) : base(maxSize){
            // Empty constructor
        }

        public PropInput TakeInput()
        {
            var str = Take();
            var ret = str switch
            {
                "w" => PropInput.ACCELERATE,
                "s" => PropInput.STOP_ACCELERATE,
                "a" => PropInput.TURN_LEFT,
                "d" => PropInput.TURN_RIGHT,
                " " => PropInput.SHOOT,
                _ => PropInput.NOTHING,
            };
            return ret;
        }
    }
}
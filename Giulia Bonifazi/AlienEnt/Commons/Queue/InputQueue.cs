using System.Collections.Concurrent;
using AlienEnt.Props;

namespace AlienEnt.Commons.Queue 
{
    public class InputQueue : BlockingCollection<string>
    {

        public InputQueue(int maxSize) : base(maxSize){
            
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
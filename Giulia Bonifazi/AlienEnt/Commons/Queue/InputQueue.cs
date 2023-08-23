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
            switch (str)
            {
                case "w":
                    return PropInput.ACCELERATE;
                case "s":
                    return PropInput.STOP_ACCELERATE;
                case "a":
                    return PropInput.TURN_LEFT;
                case "d":
                    return PropInput.TURN_RIGHT;
                case " ":
                    return PropInput.SHOOT;
                default:
                    return PropInput.NOTHING;
            }
        }
    }
}
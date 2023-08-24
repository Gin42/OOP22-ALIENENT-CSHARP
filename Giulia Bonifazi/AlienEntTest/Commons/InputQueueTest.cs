using AlienEnt.Commons.Queue;
using AlienEnt.Props;

namespace AlienEntTest.Commons
{
    [TestClass]
    public class InputQueueTest
    {
        private readonly static int s_maxSize = 3;
        private readonly static List<PropInput> s_expectedPolling = new(){PropInput.ACCELERATE, PropInput.SHOOT,
                                                                 PropInput.STOP_ACCELERATE};
        private readonly static List<PropInput> s_expectedValue = new(){PropInput.ACCELERATE, PropInput.SHOOT,
                PropInput.STOP_ACCELERATE, PropInput.NOTHING, PropInput.TURN_LEFT, PropInput.TURN_RIGHT};

        [TestMethod]
        public void QueueingAndPollingTest()
        {
            InputQueue inputQueue = new(s_maxSize);
            List<PropInput> actual = new();
            Thread thread = new(() => {
                inputQueue.Add("w");
                inputQueue.Add(" ");
                inputQueue.Add("s");
                inputQueue.Add("h");
                return;
            });
            thread.Start();
            // Wait for the thread to be done with its work.
            Thread.Sleep(1000);
            Assert.AreEqual(s_maxSize, inputQueue.Count);

            while (inputQueue.Count > 0)
            {
                actual.Add(inputQueue.TakeInput());
            }
            Assert.IsTrue(s_expectedPolling.SequenceEqual(actual));
        }

        [TestMethod]
        public void ValueTest()
        {
            InputQueue inputQueue = new(s_maxSize);
            List<PropInput> actual = new();
            Thread thread = new(() => {
                inputQueue.Add("w");
                actual.Add(inputQueue.TakeInput());
                inputQueue.Add(" ");
                actual.Add(inputQueue.TakeInput());
                inputQueue.Add("s");
                actual.Add(inputQueue.TakeInput());
                inputQueue.Add("h");
                actual.Add(inputQueue.TakeInput());
                inputQueue.Add("a");
                actual.Add(inputQueue.TakeInput());
                inputQueue.Add("d");
                actual.Add(inputQueue.TakeInput());
                return;
            });
            thread.Start();
            // Wait for the thread to finish working.
            Thread.Sleep(1000);
            Assert.IsTrue(s_expectedValue.SequenceEqual(actual));
        }
    }
}
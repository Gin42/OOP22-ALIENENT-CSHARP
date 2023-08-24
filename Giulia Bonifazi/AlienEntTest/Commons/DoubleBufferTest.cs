using AlienEnt.Commons.Buffer;

namespace AlienEntTest.Commons
{
    [TestClass]
    public class DoubleBufferTest
    {
        private static readonly ISet<int> s_expected =  new HashSet<int>() { 1, 2, 3, 4 };
        private readonly IDoubleBuffer<int> _doubleBuffer;

        public DoubleBufferTest()
        {
            _doubleBuffer = new DoubleBuffer<int>();
        }

        [TestMethod]
        public void TestDoubleBuffer()
        {
            _doubleBuffer.Buffer.Add(1);
            _doubleBuffer.Buffer.Add(2);
            _doubleBuffer.ChangeBuffer();
            _doubleBuffer.Buffer.Add(3);
            _doubleBuffer.Buffer.Add(4);
            _doubleBuffer.ChangeBuffer();
            Assert.IsTrue(_doubleBuffer.Buffer.SequenceEqual(_doubleBuffer.Current));
            Assert.IsTrue(s_expected.SequenceEqual(_doubleBuffer.Current));
        }
    }
}
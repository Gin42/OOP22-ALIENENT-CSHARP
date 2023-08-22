
namespace Buffer {

    // Implementation of the DoubleBuffer interface.
    class DoubleBuffer<T> : IDoubleBuffer<T>
    {
        public ISet<T> Buffer {private set; get;}

        public ISet<T> Current {private set; get;}

        public DoubleBuffer() {
            Current = new HashSet<T>();
            Buffer = new HashSet<T>();
        }

        public void ChangeBuffer()
        {
            Current = Buffer;
            Buffer = new HashSet<T>(Current);
        }
    }
}
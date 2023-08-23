
namespace AlienEnt.Commons.Buffer {

    // Implementation of the DoubleBuffer interface.
    class DoubleBuffer<T> : IDoubleBuffer<T>
    {
        public DoubleBuffer() 
        {
            Current = new HashSet<T>();
            Buffer = new HashSet<T>();
        }
        public ISet<T> Buffer {private set; get;}
        public ISet<T> Current {private set; get;}

        public void ChangeBuffer()
        {
            Current = Buffer;
            Buffer = new HashSet<T>(Current);
        }
    }
}
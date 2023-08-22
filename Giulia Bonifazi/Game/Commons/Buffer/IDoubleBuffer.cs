namespace Buffer {
    // DoubleBuffer interface.
    public interface IDoubleBuffer<T> {
        ISet<T> Buffer {get;}
        ISet<T> Current {get;}

        // Swaps the two sets.
        void ChangeBuffer();
    }
}
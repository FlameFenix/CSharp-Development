namespace Problem03.Queue
{
    public class Node<T>
    {
        public Node(T item, Node<T> next)
        {
            Item = item;
            Next = next;
        }

        public T Item { get; set; }

        public Node<T> Next { get; set; }
    }
}
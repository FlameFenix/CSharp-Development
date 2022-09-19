namespace Problem04.SinglyLinkedList
{
    public class Node<T>
    {
        public Node()
        {

        }
        public Node(T item, Node<T> next, Node<T> previous)
        {
            Item = item;
            Next = next;
            Previous = previous;
        }

        public T Item { get; set; }

        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }
    }
}
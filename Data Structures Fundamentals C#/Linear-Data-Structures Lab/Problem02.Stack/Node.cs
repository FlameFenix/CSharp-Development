namespace Problem02.Stack
{
    public class Node<T>
    {
        // TODO: Implement
        private T _item;
        private Node<T> _next;

        public Node(T item, Node<T> next)
        {
            _item = item;
            _next = next;
        }

        public T Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public Node<T> Next
        {
            get { return _next; }
            set { _next = value; }
        }
    }
}
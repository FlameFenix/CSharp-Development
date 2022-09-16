namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public Stack()
        {
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var currentNode = _top;

            while (currentNode != null)
            {
                if (currentNode.Item.Equals(item))
                {
                    return true;
                }

                currentNode = currentNode.Next;
            }

            return false;
        }

        public T Peek()
        {
            EnsureIsNotEmpty();

            return _top.Item;
        }

        public T Pop()
        {
            EnsureIsNotEmpty();

            var oldTopElement = _top;

            _top = _top.Next;

            Count--;

            return oldTopElement.Item;

        }

        public void Push(T item)
        {
            var newNode = new Node<T>(item, _top);

            if (_top == null)
            {
                _top = newNode;
            }
            else
            {
                var oldNode = _top;
                _top = newNode;
                _top.Next = oldNode;
            }

            Count++;
        }

        private void EnsureIsNotEmpty()
        {

            if (_top == null)
            {
                throw new InvalidOperationException(string.Format("Stack is empty!"));
            }

        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = _top;

            while (node != null)
            {
                yield return node.Item;

                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
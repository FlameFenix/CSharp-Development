namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var node = _head;

            while (node != null)
            {
                if (node.Item.Equals(item))
                {
                    return true;
                    break;
                }
                node = node.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            EnsureIsNotEmpty();
            var oldHead = _head;
            _head = _head.Next;
            Count--;
            return oldHead.Item;
        }

        public void Enqueue(T item)
        {
            var node = new Node<T>(item, null, null);

            if(_head == null)
            {
                _head = node;
                _tail = node;
            }
            else
            {
                _tail.Next = node;
                _tail.Previous = _tail;
                _tail = _tail.Next;
            }

            Count++;
        }

        public T Peek()
        {
            EnsureIsNotEmpty();
            return _head.Item;
        }

        private void EnsureIsNotEmpty()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = _head;

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
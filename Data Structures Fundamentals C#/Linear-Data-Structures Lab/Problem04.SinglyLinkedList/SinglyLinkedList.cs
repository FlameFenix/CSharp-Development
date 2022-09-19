namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;
        private Node<T> _tail;



        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item, null);

            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                var oldHead = _head;
                _head = newNode;
                _head.Next = oldHead;
            }

            Count++;

        }

        public void AddLast(T item)
        {
            var newNode = new Node<T>(item, null);

            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                _tail = _tail.Next;
            }

            Count++;
        }

        public T GetFirst()
        {
            IsEmpty();

            return _head.Item;
        }

        public T GetLast()
        {
            IsEmpty();

            return _tail.Item;
        }

        public T RemoveFirst()
        {
            IsEmpty();

            var oldHead = _head;
            _head = _head.Next;
            Count--;
            return oldHead.Item;
        }

        public T RemoveLast()
        {
            IsEmpty();

            return _tail.Item;
        }

        private void IsEmpty()
        {
            if(Count == 0)
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
            => this.GetEnumerator();
    }
}
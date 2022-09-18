namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
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
                }

               node = node.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            if(Count == 0)
            {
                throw new InvalidOperationException();
            }

            var oldHead = _head;
            _head = _head.Next;
            Count--;
            return oldHead.Item;
        }

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item, null);

            if(_head == null)
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

        public T Peek()
        {
            if(Count == 0)
            {
                throw new InvalidOperationException();
            }
            return _head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = _head;

            while (currentNode != null)
            {
                yield return currentNode.Item;

                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
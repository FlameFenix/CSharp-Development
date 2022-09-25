namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item, null, null);

            if(Count == 0)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                var oldHead = head;
                head = newNode;
                head.Next = oldHead;
                head.Next.Previous = head;
            }

            Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node<T>(item, null, null);

            if (Count == 0)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                var oldNode = tail;
                tail.Next = newNode;
                tail = tail.Next;
                tail.Previous = oldNode;
            }

            Count++;
        }

        public T GetFirst()
        {
            EnsureIsNotEmpty();
            return head.Item;
        }

        public T GetLast()
        {
            EnsureIsNotEmpty();
            return tail.Item;
        }

        public T RemoveFirst()
        {
            EnsureIsNotEmpty();

            var oldNode = head;
            head = head.Next;
            Count--;

            return oldNode.Item;
        }



        public T RemoveLast()
        {
            EnsureIsNotEmpty();

            var oldTail = tail;
            tail = tail.Previous;
            Count--;
            return oldTail.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = head;

            while (node != null)
            {
                yield return node.Item;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private void EnsureIsNotEmpty()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
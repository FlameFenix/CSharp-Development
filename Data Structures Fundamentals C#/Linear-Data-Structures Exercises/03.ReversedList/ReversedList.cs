namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return _items[Count - index - 1];
            }
            set
            {
                ValidateIndex(index);
                _items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if(Count == _items.Length)
            {
                ArrayGrow();
            }

            _items[Count++] = item;
        }

        

        public bool Contains(T item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (_items[i].Equals(item))
                {
                    return Count - i - 1;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private void ArrayGrow()
        {
            T[] newArray = new T[_items.Length * 2];

            Array.Copy(_items, newArray, _items.Length);

            _items = newArray;
        }

        private void ValidateIndex(int index)
        {
            if(index < 0 || index > Count - 1)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
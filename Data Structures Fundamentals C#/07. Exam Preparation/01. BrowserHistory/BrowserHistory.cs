namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private ILink[] links;
        private int arrayCapacity = 4;

        public BrowserHistory()
        {
            links = new ILink[arrayCapacity];
        }

        public ILink this[int index]
        {
            get
            {
                // 5 4 3 2 1
                ValidateIndex(index);
                return links[Size - index];
            }
            set
            {
                // 1 2 3 4 5
                ValidateIndex(index);
                links[index] = value;
            }
        }

        private void ValidateIndex(int index)
        {
            if(index < 0 && index > Size)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public int Size { get; private set; }

        public void Clear()
        {
            links = default;
            Size = 0;
        }

        public bool Contains(ILink link)
        {
            foreach (var linkItem in links)
            {
                if (linkItem.Equals(link))
                {
                    return true;
                }
            }

            return false;
        }

        public ILink DeleteFirst()
        {
            var oldLink = links[0];

            for (int i = 0; i < Size - 1; i++)
            {
                links[i] = links[i + 1];
            }

            links[Size - 1] = default;

            Size--;

            return oldLink;
        }

        public ILink DeleteLast()
        {
            var oldLink = links[Size - 1];
            links[Size - 1] = default;
            Size--;

            return oldLink;
        }

        public ILink GetByUrl(string url)
        {
            foreach (var link in links)
            {
                if (link.Url.Equals(url))
                {
                    return link;
                }
            }

            return null;
        }

        public ILink LastVisited()
        {
            if(Size == 0)
            {
                throw new InvalidOperationException();
            }

            return links[Size - 1];
        }

        public void Open(ILink link)
        {
            if(links.Length == Size)
            {
                arrayGrow();
            }

            links[Size++] = link;
        }

        private void arrayGrow()
        {
            var array = new ILink[links.Length * 2];
            Array.Copy(links, array, Size);
            links = array;
        }

        public int RemoveLinks(string url)
        {
            throw new NotImplementedException();
        }

        public ILink[] ToArray()
        {
            throw new NotImplementedException();
        }

        public List<ILink> ToList()
        {
            var list = new List<ILink>(links.Length);

            for (int i = Size - 1; i >= 0; i--)
            {
                list.Add(links[i]);
            }

            return list;
        }

        public string ViewHistory()
        {
            throw new NotImplementedException();
        }
    }
}

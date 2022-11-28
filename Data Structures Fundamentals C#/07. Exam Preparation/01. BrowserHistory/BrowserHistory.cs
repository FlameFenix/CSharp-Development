namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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
            CheckCollection();

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
            CheckCollection();

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
            CheckCollection();

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
            int counter = 0;

            var index = indexOf(url);

            while (index != -1)
            {
                for (int i = index; i < Size - 1; i++)
                {
                    links[i] = links[i + 1];
                }

                links[Size - 1] = default;
                Size--;
                counter++;

                index = indexOf(url);
            }

            if(counter == 0)
            {
                throw new InvalidOperationException();
            }

            return counter;
        }

        public ILink[] ToArray()
        {
            var array = new ILink[Size];

            for (int i = 0; i < Size; i++)
            {
                array[i] = links[Size - 1 - i];
            }

            return array;
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
            var sb = new StringBuilder();

            if(Size == 0)
            {
                sb.AppendLine("Browser history is empty!");
                return sb.ToString();
            }

            for (int i = Size - 1; i >= 0; i--)
            {
                sb.AppendLine($"{links[i]}");
            }

            return sb.ToString();
        }

        private void CheckCollection()
        {
            if(Size == 0)
            {
                throw new InvalidOperationException();
            }
        }

        private int indexOf(string url)
        {
            for (int i = 0; i < Size; i++)
            {
                if (links[i].Url.ToLower().Contains(url.ToLower()))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}

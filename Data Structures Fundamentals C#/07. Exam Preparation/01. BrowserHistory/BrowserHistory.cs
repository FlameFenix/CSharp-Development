namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private LinkedList<ILink> history = new LinkedList<ILink>();
        public int Size { get; private set; }

        public void Clear()
        {
            history.Clear();
            Size = 0;
        }

        public bool Contains(ILink link)
        {
            return history.Contains(link);
        }

        public ILink DeleteFirst()
        {
            var link = history.LastOrDefault();

            if (link == null)
            {
                throw new InvalidOperationException();
            }

            history.RemoveFirst();

            Size--;

            return link;
        }

        public ILink DeleteLast()
        {
            var link = history.FirstOrDefault();

            if (link == null)
            {
                throw new InvalidOperationException();
            }

            history.RemoveLast();

            Size--;

            return link;
        }

        public ILink GetByUrl(string url)
        {
            return history.Where(x => x.Url.ToLower().Equals(url.ToLower())).FirstOrDefault();
        }

        public ILink LastVisited()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException();
            }

            return history.FirstOrDefault();
        }

        public void Open(ILink link)
        {
            history.AddFirst(link);
            Size++;
        }

        public int RemoveLinks(string url)
        {

            
            var deletedLinks = history.Where(x => x.Url.Contains(url)).ToArray();

            if (deletedLinks.Length == 0)
            {
                throw new InvalidOperationException();
            }

            var oldCount = Size;

            foreach (var link in deletedLinks)
            {
                history.Remove(link);
                Size--;
            }

            return oldCount - Size;
        }

        public ILink[] ToArray()
        {
            return history.ToArray();
        }

        public List<ILink> ToList()
        {
            return history.ToList();
        }

        public string ViewHistory()
        {
            var sb = new StringBuilder();

            var currentHistory = ToList();

            if (currentHistory.Count == 0)
            {
                sb.AppendLine("Browser history is empty!");
            }

            foreach (var link in currentHistory)
            {
                sb.AppendLine(link.ToString());
            }

            
            return sb.ToString();
        }
    }
}

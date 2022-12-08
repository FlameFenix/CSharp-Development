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
        public int Size => history.Count;

        public void Clear()
        {
            history.Clear();
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
        }

        public int RemoveLinks(string url)
        {
            var deletedLinks = history.Where(x => x.Url.Contains(url)).ToArray();

            if (deletedLinks.Length == 0)
            {
                throw new InvalidOperationException();
            }

            var deletedCount = deletedLinks.Length;

            foreach (var link in deletedLinks)
            {
                history.Remove(link);
            }

            return deletedCount;
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

            if (Size == 0)
            {
                sb.AppendLine("Browser history is empty!");
            }

            foreach (var link in history)
            {
                sb.AppendLine(link.ToString());
            }

            
            return sb.ToString();
        }
    }
}

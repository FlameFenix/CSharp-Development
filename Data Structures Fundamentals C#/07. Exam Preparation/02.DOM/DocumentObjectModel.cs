namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using _02.DOM.Interfaces;
    using _02.DOM.Models;

    public class DocumentObjectModel : IDocument
    {
        private IHtmlElement root;

        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;
        }

        public DocumentObjectModel()
        {
            Root = root;
        }

        public IHtmlElement Root { get; private set; }

        public IHtmlElement GetElementByType(ElementType type)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();

                if (currentElement.Type.Equals(type))
                {
                    return currentElement;
                }

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public List<IHtmlElement> GetElementsByType(ElementType type) // should be dfs !
        {
            var list = new List<IHtmlElement>();

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();

                if (currentElement.Type.Equals(type))
                {
                    list.Add(currentElement);
                }

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return list;
        }

        public bool Contains(IHtmlElement htmlElement)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var Parent = queue.Dequeue();

                if (Parent.Equals(htmlElement))
                {
                    return true;
                }

                foreach (var child in Parent.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return false;
        }


        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            CheckElementExist(parent);

            child.Parent = parent;
            parent.Children.Insert(0, child);
        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            CheckElementExist(parent);

            child.Parent = parent;
            parent.Children.Add(child);
        }

        public void Remove(IHtmlElement htmlElement)
        {
            CheckElementExist(htmlElement);

            throw new NotImplementedException();
        }

        public void RemoveAll(ElementType elementType)
        {
            throw new NotImplementedException();
        }

        public bool AddAttribute(string attrKey, string attrValue, IHtmlElement htmlElement)
        {
            CheckElementExist(htmlElement);

            if (htmlElement.Attributes.ContainsKey(attrKey))
            {
                return false;
            }

            htmlElement.Attributes.Add(attrKey, attrValue);

            return true;
        }



        public bool RemoveAttribute(string attrKey, IHtmlElement htmlElement)
        {
            CheckElementExist(htmlElement);

            if (htmlElement.Attributes.ContainsKey(attrKey))
            {
                htmlElement.Attributes.Remove(attrKey);

                return true;
            }

            return false;
        }

        public IHtmlElement GetElementById(string idValue)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();

                if (currentElement.Attributes.ContainsKey("id") 
                    && currentElement.Attributes["id"].Equals(idValue))
                {
                    return currentElement;
                }

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void CheckElementExist(IHtmlElement htmlElement)
        {
            if (!Contains(htmlElement))
            {
                throw new InvalidOperationException();
            }
        }
    }
}

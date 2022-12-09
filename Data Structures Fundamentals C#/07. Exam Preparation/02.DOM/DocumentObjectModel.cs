namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using _02.DOM.Interfaces;
    using _02.DOM.Models;

    public class DocumentObjectModel : IDocument
    {
        private IHtmlElement root;
        private LinkedList<IHtmlElement> elements = new LinkedList<IHtmlElement>();

        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;

            initRoot(root, root.Children);
        }

        private void initRoot(IHtmlElement root, List<IHtmlElement> children)
        {
            elements.AddLast(root);

            foreach (var child in children)
            {
                child.Parent = root;
                initRoot(child, child.Children);
            }
        }
        public DocumentObjectModel()
        {
            Root = root;
        }

        public IHtmlElement Root { get; private set; }

        public IHtmlElement GetElementByType(ElementType type)
         => elements.FirstOrDefault(x => x.Type.Equals(type));
        

        public List<IHtmlElement> GetElementsByType(ElementType type) // should be dfs !
        {
            var list = new List<IHtmlElement>();

            GetElementsByTypeDfs(Root, list, type);

            return list;
        }

        private void GetElementsByTypeDfs(IHtmlElement root, List<IHtmlElement> list, ElementType type)
        {
            foreach (var child in root.Children)
            {
                GetElementsByTypeDfs(child, list, type);
            }

            if (root.Type.Equals(type))
            {
                list.Add(root);
            }
        }

        public bool Contains(IHtmlElement htmlElement)
            => elements.Contains(htmlElement);
        
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

            elements.Remove(htmlElement);
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

        public override string ToString()
        {
            var sb = new StringBuilder();

            ToStringDfs(sb, this.Root, 0);

            return sb.ToString();
        }

        private void ToStringDfs(StringBuilder sb, IHtmlElement htmlElement, int indent)
        {
            sb.Append(' ', indent)
              .AppendLine(htmlElement.Type.ToString());

            foreach (var child in htmlElement.Children)
            {
                ToStringDfs(sb, child, indent + 2);
            }
        }

        public IHtmlElement GetElementById(string idValue)
            => elements.FirstOrDefault(x => x.Attributes.ContainsKey("id"));

        private void CheckElementExist(IHtmlElement htmlElement)
        {
            if (!Contains(htmlElement))
            {
                throw new InvalidOperationException();
            }
        }


    }
}

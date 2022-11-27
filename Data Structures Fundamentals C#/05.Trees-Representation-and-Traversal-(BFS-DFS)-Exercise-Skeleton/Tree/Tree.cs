namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;
        public Tree(T key, params Tree<T>[] children)
        {
            Key = key;
            this.children = children.ToList();
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            Parent = parent;
        }

        public string AsString()
        {
            var sb = new StringBuilder();

            DfsToString(this, sb, 0);

            return sb.ToString().Trim();
        }
        public IEnumerable<T> GetInternalKeys()
        {
            var list = new List<T>();

            DfsInternalKeys(this, list);

            return list;
        }

        public IEnumerable<T> GetLeafKeys()
        {
            var list = new List<T>();

            DfsLeafKeys(list, this);

            return list;
        }

        public T GetDeepestKey()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetLongestPath()
        {
            throw new NotImplementedException();
        }

        private void DfsToString(Tree<T> tree, StringBuilder sb, int indent)
        {
            sb.Append(' ', indent).AppendLine(tree.Key.ToString());

            foreach (var child in tree.Children)
            {
                DfsToString(child, sb, indent + 2);
            }
        }

        private void DfsLeafKeys(List<T> list, Tree<T> tree)
        {

            foreach (var child in tree.children)
            {
                if (!child.children.Any())
                {
                    list.Add(child.Key);

                }

                DfsLeafKeys(list, child);
            }


        }

        private void DfsInternalKeys(Tree<T> tree, List<T> list)
        {
            foreach (var child in tree.Children)
            {
                if (child.Children.Any())
                {
                    list.Add(child.Key);
                }

                DfsInternalKeys(child, list);
            }
        }
    }
}
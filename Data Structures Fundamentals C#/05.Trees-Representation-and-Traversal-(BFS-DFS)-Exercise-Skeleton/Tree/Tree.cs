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
            var list = new List<Tree<T>>();

            DfsLeafKeys(list, this);

            return list.Select(x => x.Key);
        }

        public T GetDeepestKey()
        {
            var leafs = new List<Tree<T>>();
            DfsLeafKeys(leafs, this);

            var result = DeepestKey(leafs);

            return result;
        }

        private T DeepestKey(List<Tree<T>> leafs)
        {
            var maxDepth = 0;
            T deepestLeaf = default;

            foreach (var leaf in leafs)
            {
                var depth = GetDepth(leaf);

                if(depth > maxDepth)
                {
                    maxDepth = depth;
                    deepestLeaf = leaf.Key;
                }
            }

            return deepestLeaf;
        }

        private int GetDepth(Tree<T> leaf)
        {
            int counter = 0;

            while (leaf != null)
            {
                counter++;
                leaf = leaf.Parent;
            }

            return counter;
        }

        public IEnumerable<T> GetLongestPath()
        {
            var result = new List<Tree<T>>();

            DfsLeafKeys(result, this);

            return GetLongest(result).Select(x => x.Key).Reverse();
        }

        private List<Tree<T>> GetLongest(List<Tree<T>> result)
        {
            var longestPath = 0;
            var longestCollection = new List<Tree<T>>();

            foreach (var leaf in result)
            {
                var currentCollection = GetNodePathLength(leaf);

                if(currentCollection.Count > longestPath)
                {
                    longestCollection = currentCollection;
                    longestPath = currentCollection.Count;
                }
            }

            return longestCollection;
        }

        private List<Tree<T>> GetNodePathLength(Tree<T> leaf)
        {
            var list = new List<Tree<T>>();

            while (leaf != null)
            {
                list.Add(leaf);
                leaf = leaf.Parent;
            }

            return list;
        }

        private void DfsToString(Tree<T> tree, StringBuilder sb, int indent)
        {
            sb.Append(' ', indent).AppendLine(tree.Key.ToString());

            foreach (var child in tree.Children)
            {
                DfsToString(child, sb, indent + 2);
            }
        }

        private void DfsLeafKeys(List<Tree<T>> list, Tree<T> tree)
        {

            foreach (var child in tree.children)
            {
                if (!child.children.Any())
                {
                    list.Add(child);

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
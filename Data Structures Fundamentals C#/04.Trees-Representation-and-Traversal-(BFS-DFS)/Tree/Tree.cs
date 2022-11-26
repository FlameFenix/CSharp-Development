namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private T value;
        private Tree<T> parent;
        private List<Tree<T>> children;

        public Tree(T value)
        {
            this.value = value;
            children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.parent = this;
                this.children.Add(child);
            }
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var queue = new Queue<Tree<T>>();
            var isExist = false;

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var parent = queue.Dequeue();

                if (parent.value.Equals(parentKey))
                {
                    child.parent = parent;
                    parent.children.Add(child);
                    isExist = true;
                }

                foreach (var currentChild in parent.children)
                {
                    queue.Enqueue(currentChild);
                }
            }

            if (!isExist)
            {
                throw new ArgumentNullException();
            }

        }

        public IEnumerable<T> OrderBfs()
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<T>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                result.Add(tree.value);

                foreach (var child in tree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            var result = new List<T>();

            DeepFirstSearch(result, this);

            return result;

        }

        private void DeepFirstSearch(List<T> result, Tree<T> node)
        {
            foreach (var child in node.children)
            {
                DeepFirstSearch(result, child);
            }

            result.Add(node.value);
        }

        public void RemoveNode(T nodeKey)
        {
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            bool isRemoved = false;

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();

                if (tree.value.Equals(nodeKey))
                {
                    var parent = tree.parent;

                    if (parent == null)
                    {
                        throw new ArgumentException();
                    }

                    tree.parent = null;
                    parent.children.Remove(tree);
                    isRemoved = true;
                    break;
                }

                foreach (var child in tree.children)
                {
                    queue.Enqueue(child);
                }

            }

            if (!isRemoved)
            {
                throw new ArgumentNullException();
            }
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstChildren = FindChild(firstKey);
            var secondChildren = FindChild(secondKey);

            var firstParent = firstChildren.parent;
            var secondParent = secondChildren.parent;

            if (firstParent == null || secondParent == null)
            {
                throw new ArgumentException();
            }

            var firstIndex = firstParent.children.IndexOf(firstChildren);
            var secondIndex = secondParent.children.IndexOf(secondChildren);

            firstParent.children[firstIndex] = secondChildren;
            secondParent.children[secondIndex] = firstChildren;
        }

        private Tree<T> FindChild(T nodeKey)
        {
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            Tree<T> result = null;

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();

                if (tree.value.Equals(nodeKey))
                {
                    result = tree;
                    return result;
                }

                foreach (var child in tree.children)
                {
                    queue.Enqueue(child);
                }

            }

            if (result == null)
            {
                throw new ArgumentNullException();
            }

            return result;
        }
    }
}

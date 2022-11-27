namespace Tree
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class IntegerTreeFactory
    {
        private Dictionary<int, IntegerTree> nodesByKey;

        public IntegerTreeFactory()
        {
            this.nodesByKey = new Dictionary<int, IntegerTree>();
        }

        public IntegerTree CreateTreeFromStrings(string[] input)
        {
            foreach (var inputLine in input)
            {
                var currentLine = inputLine.Split(' ').Select(x => int.Parse(x)).ToArray();

                int parent = currentLine[0];
                int child = currentLine[1];

                this.AddEdge(parent, child);
            }

            return GetRoot();
        }

        public IntegerTree CreateNodeByKey(int key)
        {
            if (!nodesByKey.ContainsKey(key))
            {
                nodesByKey.Add(key, new IntegerTree(key));
            }

            return nodesByKey[key];

        }

        public void AddEdge(int parent, int child)
        {
            var parentNode = CreateNodeByKey(parent);
            var childNode = CreateNodeByKey(child);

            childNode.AddParent(parentNode);
            parentNode.AddChild(childNode);
        }

        public IntegerTree GetRoot()
        {
            foreach (var kvp in nodesByKey)
            {
                var parrent = kvp.Value.Parent;

                if (parrent == null)
                {
                    return kvp.Value;
                }
            }

            return null;
        }
    }
}

namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        private List<IAbstractBinaryTree<T>> list;
        public BinaryTree(T element, IAbstractBinaryTree<T> left, IAbstractBinaryTree<T> right)
        {
            Value = element;
            LeftChild = left;
            RightChild = right;
            list = new List<IAbstractBinaryTree<T>>();
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            var sb = new StringBuilder();

            PreOrderToString(sb, 0, this);

            return sb.ToString().Trim();
        }

        private void PreOrderToString(StringBuilder sb, int indent, IAbstractBinaryTree<T> binaryTree)
        {
            sb.Append(' ', indent)
              .AppendLine(binaryTree.Value.ToString());
            
            if(binaryTree.LeftChild != null)
            {
                PreOrderToString(sb, indent + 2, binaryTree.LeftChild);
            }

            if(binaryTree.RightChild != null)
            {
                PreOrderToString(sb, indent + 2, binaryTree.RightChild);
            }
        }

        public void ForEachInOrder(Action<T> action)
        {
            if(LeftChild != null)
            {
                LeftChild.ForEachInOrder(action);
            } 

            action(this.Value);

            if(RightChild != null)
            {
                RightChild.ForEachInOrder(action);
            }
        
        }

        public IEnumerable<IAbstractBinaryTree<T>> InOrder()
        {
            
            if(LeftChild != null)
            {
                list.AddRange(LeftChild.InOrder());
            }


            list.Add(this);

            if (RightChild != null)
            {
                list.AddRange(RightChild.InOrder());

            }

            return list;
        }


        public IEnumerable<IAbstractBinaryTree<T>> PostOrder()
        {

            if(LeftChild != null)
            {
                list.AddRange(LeftChild.PostOrder());
            }

            if(RightChild != null)
            {
                list.AddRange(RightChild.PostOrder());
            }

            list.Add(this);

            return list;
        }


        public IEnumerable<IAbstractBinaryTree<T>> PreOrder()
        {
            list.Add(this);

            if (LeftChild != null)
            {
                list.AddRange(LeftChild.PreOrder());
            }

            if (RightChild != null)
            {
                list.AddRange(RightChild.PreOrder());
            }

            return list;
        }

    }
}

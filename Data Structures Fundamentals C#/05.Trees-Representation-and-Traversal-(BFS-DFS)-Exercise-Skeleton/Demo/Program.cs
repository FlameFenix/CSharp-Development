namespace Demo
{
    using System;
    using Tree;

    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> tree = new Tree<int>(10, new Tree<int>(15, new Tree<int>(3)));

            tree.AddChild(new Tree<int>(11));

            Console.WriteLine(tree.AsString());
            Console.WriteLine(string.Join(' ', tree.GetLeafKeys()));
            Console.WriteLine(string.Join(' ', tree.Children));
        }
    }
}

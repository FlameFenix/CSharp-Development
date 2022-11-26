namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var tree = new Tree<int>(10, new Tree<int>(15), new Tree<int>(3, new Tree<int>(5)));
            tree.Swap(15, 3);

            Console.WriteLine(string.Join(' ', tree.OrderBfs()));
        }
    }
}

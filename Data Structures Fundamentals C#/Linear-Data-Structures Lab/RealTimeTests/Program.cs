
using Problem02.Stack;
// See https://aka.ms/new-console-template for more information
Problem02.Stack.Stack<int> stack = new Problem02.Stack.Stack<int>();
stack.Push(1);
stack.Push(2);
stack.Push(3);
stack.Push(4);
Console.WriteLine($"POP {stack.Pop()}");

foreach (int i in stack)
{
    Console.WriteLine(i);
}

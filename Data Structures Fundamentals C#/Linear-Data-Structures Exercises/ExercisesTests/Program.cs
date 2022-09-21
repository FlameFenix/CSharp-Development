// See https://aka.ms/new-console-template for more information


using Problem01.FasterQueue;

FastQueue<int> fq = new FastQueue<int>();

fq.Enqueue(1);
fq.Enqueue(2);
fq.Enqueue(3);

foreach (var item in fq)
{
    Console.WriteLine(item);
}

Console.WriteLine(fq.Dequeue());

Console.WriteLine(String.Join(' ', fq));
using _01._BrowserHistory;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var browser = new BrowserHistory();

            browser.Open(new Link("www.abv.bg", 42));
            browser.Open(new Link("www.zamunda.bg", 42));
            browser.Open(new Link("www.softuni.bg", 42));
            browser.Clear();
            Console.WriteLine(string.Join(' ', browser.ToList()));
        }
    }
}

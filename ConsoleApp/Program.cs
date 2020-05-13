using System;
using CWLibrary;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Pair<int, string> p = new Pair<int, string>(1, "2");
            Console.WriteLine(p);
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CWLibrary;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string path = "dictionary.txt";
            int n = GetNumber("Enter the number of words N");
            File.Create(path);
            for (int i = 0; i < n; i++)
            {
                File.AppendText(GetPair());
            }

            Dictionary dict = new Dictionary();
            foreach (var pair in from line in File.ReadLines(path) select line.Split())
            {
                dict.Add(pair[0], pair[1]);
            }

            const string binPath = "./out.bin";
            dict.MySerialize(binPath);
            
            Dictionary dict2 = Dictionary.MyDeserialize(binPath);

            foreach (var pair in dict2)
            {
                Console.WriteLine(pair);
            }

            char symbol = (dict2.locale == 0
                ? (char) Utils.Random.Next((int) 'а', (int) 'я' + 1) 
                : (char) Utils.Random.Next((int) 'a', (int) 'z' + 1)
                );

            Console.WriteLine($"Randomly chosen symbol: '{symbol}'");

            Console.WriteLine("To be continued...");
            // foreach (var VARIABLE in dict2.GetEnumeratorStartingWith(symbol))
            // {
            //     
            // }
        }

        public static bool IsValidWord(string word, bool russian = false)
        {
            if (russian)
                return Regex.IsMatch(word, "[а-яА-Я]+");
            return Regex.IsMatch(word, "[a-zA-Z]+");
        }

        private static string GetPair()
        {
            bool failed = false;
            string s1;

            do
            {
                if (failed)
                    Console.WriteLine("Invalid input!");
                Console.Write("Enter the first word, in Russian: ");
                s1 = Console.ReadLine();
                failed = true;
            } while (!IsValidWord(s1, true));

            failed = false;
            string s2;
            do
            {
                if (failed)
                    Console.WriteLine("Invalid input!");
                Console.Write("Enter the second word, in English: ");
                s2 = Console.ReadLine();
                failed = true;
            } while (!IsValidWord(s2));

            return $"{s1} {s2}";
        }

        private static int GetNumber(string prompt)
        {
            int n;
            string s;
            bool failed = false;

            do
            {
                if (failed)
                    Console.WriteLine("Invalid input!");
                Console.Write($"{prompt}: ");
                s = Console.ReadLine();
                failed = true;
            } while (!int.TryParse(s, out n));

            return n;
        }
    }
}
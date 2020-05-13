using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace CWLibrary
{
    [Serializable]
    public class Dictionary : IEnumerable<Pair<string, string>>
    {
        /// <summary>
        /// Supported values: <b>0</b> - ru->en, <b>1</b> - en->ru.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        // Not named "Locale" to follow the specification given in the task.
        public int locale;

        // ReSharper disable once InconsistentNaming
        private List<Pair<string, string>> words;

        public Dictionary()
        {
            words = new List<Pair<string, string>>();
        }

        public Dictionary(List<Pair<string, string>> words)
        {
            this.words = words;
            locale = Utils.Random.Next(0, 2); // [0, 2)
        }

        public void Add(Pair<string, string> pair)
        {
            if (!words.Contains(pair))
                words.Add(pair);
        }

        public void Add(string a, string b)
        {
            Add(new Pair<string, string>(a, b));
        }

        private IEnumerator<Pair<string, string>> GenericEnum(List<Pair<string, string>> list)
        {
            list.Sort((pair, pair1) =>
            {
                if (locale == 0)
                    return string.Compare(pair.item1, pair1.item1, StringComparison.Ordinal);
                return string.Compare(pair.item2, pair1.item2, StringComparison.Ordinal);
            });
            foreach (var pair in list)
            {
                yield return (Pair<string, string>) pair.Clone();
            }
        }
        
        public IEnumerator<Pair<string, string>> GetEnumerator()
        {
            var sorted = new List<Pair<string, string>>(words);
            return GenericEnum(sorted);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public IEnumerator<Pair<string, string>> GetEnumeratorStartingWith(char ch)
        {
            var sorted = new List<Pair<string, string>>(
                from word in words
                where Char.ToLower(word.item1[0]) == Char.ToLower(ch)
                select word);
            return GenericEnum(sorted);
        }

        public void MySerialize(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, this);
            fs.Close();
        }

        public static Dictionary MyDeserialize(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            
            BinaryFormatter formatter = new BinaryFormatter();
            var thing = (Dictionary)formatter.Deserialize(fs);
            fs.Close();
            return thing;
        }
    }
}
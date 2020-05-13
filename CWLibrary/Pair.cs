using System;

namespace CWLibrary
{
    public class Pair<T, U> : IComparable, ICloneable
    {
        public T item1;
        public U item2;

        public Pair(T item1, U item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }
        
        public int CompareTo(object obj)
        {
            return ((IComparable<T>) item1).CompareTo(((Pair<T, object>) obj).item1);
        }

        public override string ToString()
        {
            return $"{item1} {item2}";
        }

        public object Clone()
        {
            return new Pair<T, U>(item1, item2);
        }
    }
}
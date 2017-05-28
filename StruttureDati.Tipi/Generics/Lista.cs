using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi.Generics
{
    public class Lista<T>: FromEnumerable<T>
    {
        private T[] items;        
        public Lista(int capacity = 10)
        {
            if (capacity < 1)
                throw new ArgumentOutOfRangeException("capacity");
            items = new T[capacity];
        }
        public int Count { get; private set; }
        public void Add(T value)
        {
            if (Count == items.Length)
                Array.Resize(ref items, items.Length*2);

            items[Count] = value;
            Count++;
        }
        public T this[int index]
        {
            get {
                ValidateIndex(index);
                return items[index];
            }
            set {
                ValidateIndex(index);
                items[index] = value;
            }
        }
        
        public string[] ToArray()
        {
            var result = new string[Count];
            Array.Copy(items, result, Count);
            return result;
        }
        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("index");
        }

        #region Implementazione ITerable
        int currentIndex; 
        public void Reset()
        {
            currentIndex = 0;
        }

        public bool GetNext(out T item)
        {
            if (currentIndex >= Count)
            {
                item = default(T);
                return false;
            }
            item = items[currentIndex];
            currentIndex++;
            return true;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi.Generics
{
    public class StackArray<T>:IStack<T>, ITerable<T>
    {
        private int head;
        T[] items;
        private int capacity;

        public StackArray():this(10)
        {
        }
        public StackArray(int capacity)
        {
            this.capacity = capacity;
            items = new T[capacity];
            head = -1;
        }

        public int Count { get { return head + 1; } }
        public bool IsEmpty()
        {
            return head == -1;
        }

        public void Push(T value)
        {
            head++;
            if (head == items.Length)
                EnsureCapacity();
            items[head] = value;
        }

        public T Pop()
        {
            if (IsEmpty())
                throw new InvalidOperationException("La pila è vuota");
            return items[head--];
        }

        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("La pila è vuota");
            return items[head];
        }

        private void EnsureCapacity()
        {
            Array.Resize(ref items, items.Length * 2);
        }

        #region Implementazione ITerable
        int currentIndex;
        public void Reset()
        {
            currentIndex = head;
        }
        public bool GetNext(out T item)
        {
            if (currentIndex < 0)
            {
                item = default(T);
                return false;
            }
            item = items[currentIndex];
            currentIndex--;
            return true;
        }
        #endregion


        
    }
}

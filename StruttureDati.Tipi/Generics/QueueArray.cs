using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi.Generics
{
    public class QueueArray<T>: IQueue<T>, FromEnumerable<T>
    {
        private int head;
        private int tail;
        T[] items;

        public QueueArray():this(10)
        {
        }
        public QueueArray(int capacity)
        {
            items = new T[capacity];
            head = tail = -1;
        }

        private int capacity { get { return items.Length; } }
        public void Enqueue(T value)
        {
            if (IsFull())
                EnsureCapacity();
            else
                tail = (tail + 1) % capacity;
            items[tail] = value;
            if (head == -1)
                head = tail;
        }

        
        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Coda vuota");
            var value = items[head];
            head = (head+1) % capacity;
            if (head == (tail+1) % capacity)
            {
                head = -1;
                tail = -1;
            }
            return value;
        }

        public T Peek()
        {
            return items[head];
        }

        public bool IsEmpty()
        {
            return head == -1;
        }

        private bool  IsFull()
        {
            return !IsEmpty() && head == (tail+1) % capacity;
        }

        private void EnsureCapacity()
        {
            int newCapacity = capacity * 2;
            var newItems = new T[newCapacity];

            //!sposta gli elementi tra HEAD e capacity
            Array.Copy(items, head, newItems, head, capacity-head);

            //!sposta gli elementi tra 0 e TAIL: verificare!
            if (tail < head)
            {
                Array.Copy(items, 0, newItems, capacity, tail);
                tail = capacity + tail;
            }
            else
                tail++;

            items = newItems;
        }

        #region Implementazione ITerable
        int currentIndex;
        public void Reset()
        {
            currentIndex = head;
        }
        public bool GetNext(out T item)
        {
            if (currentIndex > tail)
            {
                item = default(T);
                return false;
            }
            item = items[currentIndex];
            currentIndex = (currentIndex + 1) % capacity;
            return true;
        }
        #endregion

    }
}

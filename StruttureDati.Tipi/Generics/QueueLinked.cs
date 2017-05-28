using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi.Generics
{
    public class QueueLinked<T> : IQueue<T>, FromEnumerable<T>
    {
        private Item<T> head;
        private Item<T> tail;
        public void Enqueue(T value)
        {
            var newItem = new Item<T>(value, null);
            if (IsEmpty())
                head = tail = newItem;
            else
            {
                tail.Next = newItem;
                tail = newItem;
            }
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("La coda è vuota");
            var value = head.Value;
            head = head.Next;
            return value;
        }

        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("La coda è vuota");
            return head.Value;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        class Item<TI>
        {
            public Item(TI value, Item<TI> next)
            {
                this.Value = value;
                this.Next = next;
            }
            internal readonly TI Value;
            internal Item<TI> Next;
        }

        #region Implementazione ITerable
        Item<T> current;
        public void Reset()
        {
            current = head;
        }
        public bool GetNext(out T item)
        {
            if (current == null)
            {
                item = default(T);
                return false;
            }
            item = current.Value;
            current = current.Next;
            return true;
        }
        #endregion


    }
}

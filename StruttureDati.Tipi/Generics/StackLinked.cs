using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi.Generics
{
    public class StackLinked<T> : IStack<T>, FromEnumerable<T>
    {
        private Item<T> head;
        public bool IsEmpty()
        {
            return head == null;
        }

        public void Push(T value)
        {
            var item = new Item<T>(value, head);
            head = item;
        }

        public T Pop()
        {
            if (IsEmpty())
                throw new InvalidOperationException("La pila è vuota");
            var value = head.Value;
            head = head.Last;
            return value;
        }

        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("La pila è vuota");
            return head.Value;
        }

        class Item<TI>
        {
            public Item(TI value, Item<TI> last)
            {
                this.Value = value;         
                this.Last = last;           
            }
            internal readonly TI Value;
            internal readonly Item<TI> Last;
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
            current = current.Last;
            return true;
        }
        #endregion

    }
}

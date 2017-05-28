using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi.Generics
{
    public interface IStack<T>
    {
        void Push(T value);        
        T Pop();
        T Peek();
        bool IsEmpty();
    }

    public interface IQueue<T>
    {
        void Enqueue(T value);
        T Dequeue();
        T Peek();
        bool IsEmpty();
    }

    

}

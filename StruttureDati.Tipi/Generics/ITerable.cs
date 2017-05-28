using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi.Generics
{
    public interface ITerable<T>
    {
        void Reset();
        bool GetNext(out T item);
    }
}

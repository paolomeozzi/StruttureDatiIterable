using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi.Generics
{
    public static class ITerableExtension
    {
        #region extension methods
        public static ITerable<T> Where<T>(this ITerable<T> items, Func<T, bool> func)
        {
            return new WhereIterable<T>(items, func);
        }
        public static ITerable<TR> Select<T, TR>(this ITerable<T> items, Func<T, TR> sel)
        {
            return new SelectIterable<T, TR>(items, sel);
        }
        public static ITerable<T> Take<T>(this ITerable<T> items, int count)
        {
            return new TakeIterable<T>(items, count);
        }
        public static ITerable<T> Skip<T>(this ITerable<T> items, int count)
        {
            return new SkipIterable<T>(items, count);
        }
        public static T First<T>(this ITerable<T> items)
        {
            items.Reset();
            T item;
            var ok = items.GetNext(out item);
            if (!ok)
                throw new InvalidOperationException("La sequenza non contiene elementi");
            return item;
        }
        public static void Foreach<T>(this ITerable<T> items, Action<T> action)
        {
            items.Reset();
            T value;
            while (items.GetNext(out value))
            {
                action(value);
            }
        }
        public static int Count<T>(this ITerable<T> items)
        {
            int count = 0;
            items.Reset();
            T _;
            while (items.GetNext(out _))
                count++;
            return count;
        }
        public static double Sum<T>(this ITerable<T> items, Func<T, double> sel)
        {
            double sum = 0;
            items.Foreach(e => sum = sum + sel(e));
            return sum;
        }
        public static double Avg<T>(this ITerable<T> items, Func<T, double> sel)
        {
            double sum = 0;
            int count = 0;
            items.Reset();
            T value;
            while (items.GetNext(out value))
            {
                sum += sel(value);
                count++;
            }
            return sum / count;
        }
        #endregion

        #region conversione da/verso IEnumerable
        public static ITerable<T> ToIterable<T>(this IEnumerable<T> items)
        {
            return new ToIterable<T>(items);
        }
        public static IEnumerable<T> FromIterable<T>(this ITerable<T> items)
        {
            return new FromIterable<T>(items);
        }
        #endregion
    }

    public class WhereIterable<T> : ITerable<T>
    {
        ITerable<T> items;
        Func<T, bool> func;
        public WhereIterable(ITerable<T> items, Func<T, bool> func)
        {
            this.items = items;
            this.func = func;
        }
        
        public void Reset()
        {
            items.Reset();
        }
        public bool GetNext(out T item)
        {
            var ok = items.GetNext(out item);
            if (!ok)
                return false;

            if (func(item))
                return true;
            return GetNext(out item);
        }
    }

    public class SelectIterable<TI,T> : ITerable<T>
    {
        ITerable<TI> items;
        Func<TI, T> select;
        public SelectIterable(ITerable<TI> items, Func<TI, T> select)
        {
            this.items = items;
            this.select = select;
        }
        public void Reset()
        {
            items.Reset();
        }
        public bool GetNext(out T item)
        {
            TI value;
            var ok = items.GetNext(out value);
            if (!ok)
            {
                item = default(T);
                return false;
            }
            item = select(value);
            return true;
        }
    }

    public class TakeIterable<T> : ITerable<T>
    {
        ITerable<T> items;
        int count;
        public TakeIterable(ITerable<T> items, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Il valore deve essere maggiore di zero");
            this.items = items;
            this.count = count;
        }

        public void Reset()
        {
            items.Reset();
        }

        int currentIndex = 0;
        public bool GetNext(out T item)
        {
            if (currentIndex >= count)
            {
                item = default(T);
                return false;
            }
            currentIndex++;
            return items.GetNext(out item);
        }
    }

    public class SkipIterable<T> : ITerable<T>
    {
        ITerable<T> items;
        int skipCount;
        public SkipIterable(ITerable<T> items, int skipCount)
        {
            if (skipCount < 0)
                throw new ArgumentOutOfRangeException(nameof(skipCount), "Il valore non deve essere minore di zero");
            this.items = items;
            this.skipCount = skipCount;
        }

        public void Reset()
        {
            items.Reset();
        }
        int count = 0;
        public bool GetNext(out T item)
        {
            while (count < skipCount && items.GetNext(out item))
                count++;
            return items.GetNext(out item);
        }
    }

    public class ToIterable<T> : ITerable<T>
    {
        IEnumerable<T> items;
        IEnumerator<T> it;
        public ToIterable(IEnumerable<T> items)
        {
            this.items = items;
        }
        
        public void Reset()
        {
            it = items.GetEnumerator();
        }
        public bool GetNext(out T item)
        {
            var ok = it.MoveNext();
            if (!ok)
            {
                item = default(T);
                return false;
            }
            item = it.Current;
            return true;
        }
    }

    public class FromIterable<T> : IEnumerable<T>, IEnumerator<T>
    {
        ITerable<T> items;
        public FromIterable(ITerable<T> items)
        {
            this.items = items;
            items.Reset();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }
       
        T current;
        public T Current
        {
            get { return current; }
        }

        public void Dispose(){}

        public bool MoveNext()
        {
            var ok = items.GetNext(out current);
            return ok;
        }

        public void Reset()
        {
            items.Reset();
        }

        #region ereditate da IEnumerator (non implementate)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        object IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

    }

}

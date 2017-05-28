using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi
{
    using Generics;
    public class RandomSequence : FromEnumerable<int>
    {
        int length;
        int minValue;
        int maxValue;
        Random rnd = new Random();
        public RandomSequence(int length, int minValue = 0, int maxValue = -1)
        {
            if (maxValue < 0)
                maxValue = int.MaxValue-1;

            this.length = length;
            this.minValue = minValue;
            this.maxValue = maxValue+1;
        }
        #region Implementazione ITerable
        int count;
        public bool GetNext(out int item)
        {
            if (count >= length)
            {
                item = 0;
                return false;
            }
            item = rnd.Next(minValue, maxValue);
            count++;
            return true;
        }

        public void Reset()
        {
            count = 0;
        }
        #endregion
    }
}

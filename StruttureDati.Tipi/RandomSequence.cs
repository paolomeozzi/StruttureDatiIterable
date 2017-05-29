using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi
{
    using Generics;
    public class RandomSequence : ITerable<int>
    {
        int length;
        int minValue;
        int maxValue;
        Random rnd = new Random();
        //sequenza di elementi compresa in [minValue - maxValue[
        public RandomSequence(int length, int minValue, int maxValue)
        {
            this.length = length;
            this.minValue = minValue;
            this.maxValue = maxValue;
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

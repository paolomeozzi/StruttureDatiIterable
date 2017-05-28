using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StruttureDati.Tipi
{
    using Generics;
    public class Pets:ITerable<Pet>
    {
        Lista<Pet> pets = new Lista<Pet>();

        public void Add(Pet p)
        {
            pets.Add(p);
        }
        #region Implementazione ITerable

        public bool GetNext(out Pet item)
        {
            return pets.GetNext(out item);
        }

        public void Reset()
        {
            pets.Reset();
        }
        #endregion
    }

    public class Pet
    {
        public string  Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return string.Format("[{0} {1}]", Name, Age);
        }
    }
}

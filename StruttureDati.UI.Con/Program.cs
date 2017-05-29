using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StruttureDati.Tipi;
using StruttureDati.Tipi.Generics;
namespace StruttureDati.UI.Con
{
    class Program
    {
        static int[] dati = { 1, 2, 3, -4, -5, -6};
        static StackArray<int> stackArray = new StackArray<int>();
        static StackLinked<int> stackLinked = new StackLinked<int>();
        static QueueArray<int> queueArray = new QueueArray<int>();
        static QueueLinked<int> queueLinked = new QueueLinked<int>();
        static Lista<int> lista = new Lista<int>();
        static List<int> list = new List<int>();
        static RandomSequence rndSequence = new RandomSequence(6, 5, 10);
        static Pets pets = new Pets();
        static void Main(string[] args)
        {
            CaricaDati();
            Visualizza();
            Console.WriteLine();
        }

        static void CaricaDati()
        {
            foreach (var d in dati)
            {
                stackArray.Push(d);
                stackLinked.Push(d);
                queueArray.Enqueue(d);
                queueLinked.Enqueue(d);
                lista.Add(d);
                list.Add(d);
            }

            pets.Add(new Pet { Name = "Lampo", Age = 10 });
            pets.Add(new Pet { Name = "Fido", Age = 3 });
            pets.Add(new Pet { Name = "Jack", Age = 6 });
            pets.Add(new Pet { Name = "Lana", Age = 9 });
        }

        static void Visualizza()
        {
            //Visualizza(stackArray);
            //Visualizza(stackLinked);
            //Visualizza(queueArray);
            //Visualizza(queueLinked);
            //Visualizza(lista);            
            //Visualizza(rndSequence);
            //Visualizza(pets);
            //Visualizza(list.ToIterable());

            //var r = Filtra(lista, e => e > 2);
            var r = lista.Where(e => e > 2);
            Visualizza(new RandomSequence(10, 1, 11));
            //Visualizza(r);                
        }

        static void Visualizza<T>(ITerable<T> items)
        {
            Type t = items.GetType();
            Console.Write("\n\n{0,-15} -> ", t.Name);

            T value;
            items.Reset();
            while(items.GetNext(out value))
            {
                Console.Write("{0,10}",value);
            }
        }

        static ITerable<T> Filtra<T>(ITerable<T>items, Func<T, bool> filtro)
        {
            var lista = new Lista<T>();
            T value;
            items.Reset();
            while (items.GetNext(out value))
            {
                if (filtro(value))
                    lista.Add(value);
            }
            return lista;

        }
    }
}

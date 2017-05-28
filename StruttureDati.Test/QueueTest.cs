using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StruttureDati.Tipi.Generics;
namespace StruttureDati.Test
{
    [TestClass]
    public class QueueTest
    {
        [TestMethod]
        public void QueueArrayEmpty()
        {
            var q = new QueueArray<int>(5);
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);
            q.Enqueue(5);
            q.Enqueue(6);
            q.Dequeue();
            q.Dequeue();
            var value = q.Dequeue();
            q.Dequeue();
            q.Dequeue();
            q.Dequeue();
            Assert.AreEqual(3, value, "Valore estratto errato");
            Assert.IsTrue(q.IsEmpty(), "La coda non è vuota");
        }

        [TestMethod]
        public void QueueLinkedEmpty()
        {
            var q = new QueueLinked<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);
            q.Enqueue(5);
            q.Enqueue(6);
            q.Dequeue();
            q.Dequeue();
            var value = q.Dequeue();
            q.Dequeue();
            q.Dequeue();
            q.Dequeue();
            Assert.AreEqual(3, value, "Valore estratto errato");
            Assert.IsTrue(q.IsEmpty(), "La coda non è vuota");
        }

       
    }
}

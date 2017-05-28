using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StruttureDati.Tipi.Generics;
namespace StruttureDati.Test
{
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void StackArrayEmpty()
        {
            var s = new StackArray<int>(1);
            s.Push(1);
            s.Push(2);
            s.Push(3);
            s.Pop();
            s.Pop();
            s.Pop();
            Assert.IsTrue(s.IsEmpty(), "La pila non è vuota");
        }

        [TestMethod]
        public void StackLinkedEmpty()
        {
            var s = new StackLinked<int>();
            s.Push(1);
            s.Push(2);
            s.Push(3);
            s.Pop();
            s.Pop();
            s.Pop();
            Assert.IsTrue(s.IsEmpty(), "La pila non è vuota");
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vectors;

namespace Vectors.Tests
{
    [TestClass]
    public class VectorsTests
    {
        [TestMethod]
        public void Multiply()
        {
            Vector expected = new Vector(new double[] { 5, 10, 15 });
            Vector actual = new Vector(new double[] { 1, 2, 3 }).Multiply(5);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Add()
        {
            Vector vector1 = new Vector(new double[] { 1, 2, 3 });
            Vector vector2 = new Vector(new double[] { 1, 2, 3, 0, 0 });

            Vector actual = vector1.Add(vector2);
            Vector expected = new Vector(new double[] { 2, 4, 6, 0, 0 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Subtract()
        {
            Vector vector1 = new Vector(new double[] { 2, 4, 6, 0, 0 });
            Vector vector2 = new Vector(new double[] { 1, 2, 3 });

            Vector actual = vector1.Subtract(vector2);
            Vector expected = new Vector(new double[] { 1, 2, 3, 0, 0 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Reverse()
        {
            Vector actual = new Vector(new double[] { 1, 2, 3 }).Reverse();
            Vector expected = new Vector(new double[] { -1, -2, -3 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetLength()
        {
            int actual = (int)new Vector(new double[] { -3, 4 }).GetLength();
            int expected = 5;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HashCode()
        {
            int actual = new Vector(new double[] { 25, -10, -15, 0, 0 }).GetHashCode();
            int expected = -30505233;

            Assert.AreEqual(expected, actual);
        }
    }
}
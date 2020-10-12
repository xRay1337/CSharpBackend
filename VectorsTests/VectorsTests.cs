using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vectors;

namespace VectorsTests
{
    [TestClass]
    public class VectorsTests
    {
        [TestMethod]
        public void Multiply()
        {
            var expected = new Vector(new double[] { 5, 10, 15 });
            var actual = new Vector(new double[] { 1, 2, 3 }).Multiply(5);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Add()
        {
            var vector1 = new Vector(new double[] { 1, 2, 3 });
            var vector2 = new Vector(new double[] { 1, 2, 3, 0, 0 });

            var actual = vector1.Add(vector2);
            var expected = new Vector(new double[] { 2, 4, 6, 0, 0 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Subtract()
        {
            var vector1 = new Vector(new double[] { 2, 4, 6, 0, 0 });
            var vector2 = new Vector(new double[] { 1, 2, 3 });

            var actual = vector1.Subtract(vector2);
            var expected = new Vector(new double[] { 1, 2, 3, 0, 0 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Reverse()
        {
            var actual = new Vector(new double[] { 1, 2, 3 }).Reverse();
            var expected = new Vector(new double[] { -1, -2, -3 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetLength()
        {
            var actual = new Vector(new double[] { -3, 4 }).GetLength();
            const double expected = 5;

            Assert.AreEqual(expected, actual, double.Epsilon);
        }

        [TestMethod]
        public void HashCode()
        {
            var actual = new Vector(new double[] { 25, -10, -15, 0, 0 }).GetHashCode();
            const int expected = -30505233;

            Assert.AreEqual(expected, actual);
        }
    }
}
using System;
using System.Text;

namespace Vectors
{
    public class Vector
    {
        private double[] coordinates;

        public double this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new ArgumentException("Argument must be positive.", nameof(index));
                }

                return coordinates[index];
            }
            set
            {
                if (index < 0)
                {
                    throw new ArgumentException("Argument must be positive.", nameof(index));
                }

                coordinates[index] = value;
            }
        }

        public Vector(int size)
        {
             if (size <= 0)
            {
                throw new ArgumentException($"Size {size} <= 0.", nameof(size));
            }

            coordinates = new double[size];
        }

        public Vector(Vector vector)
        {
            coordinates = new double[vector.coordinates.Length];
            Array.Copy(vector.coordinates, coordinates, vector.coordinates.Length);
        }

        public Vector(double[] array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException("Array length must be greater than 0.", nameof(array));
            }

            coordinates = new double[array.Length];
            Array.Copy(array, coordinates, array.Length);
        }

        public Vector(int size, double[] array)
        {
            if (size <= 0)
            {
                throw new ArgumentException($"Size {size} <= 0.", nameof(size));
            }

            coordinates = new double[size];
            int minLength = Math.Min(size, array.Length);

            Array.Copy(array, coordinates, minLength);
        }

        public int GetSize()
        {
            return coordinates.Length;
        }

        public Vector Add(Vector vector)
        {
            if (coordinates.Length < vector.coordinates.Length)
            {
                Array.Resize(ref coordinates, vector.coordinates.Length);
            }

            for (int i = 0; i < vector.coordinates.Length; i++)
            {
                coordinates[i] += vector.coordinates[i];
            }

            return this;
        }

        public Vector Subtract(Vector vector)
        {
            if (coordinates.Length < vector.coordinates.Length)
            {
                Array.Resize(ref coordinates, vector.coordinates.Length);
            }

            for (int i = 0; i < vector.coordinates.Length; i++)
            {
                coordinates[i] -= vector.coordinates[i];
            }

            return this;
        }

        public Vector Multiply(double scalar)
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] *= scalar;
            }

            return this;
        }

        public Vector Reverse()
        {
            return Multiply(-1);
        }

        public double GetLength()
        {
            double squareSum = 0;

            foreach (var e in coordinates)
            {
                squareSum += Math.Pow(e, 2);
            }

            return Math.Sqrt(squareSum);
        }

        public double GetElement(int index)
        {
            return coordinates[index];
        }

        public Vector SetElement(int index, double element)
        {
            coordinates[index] = element;

            return this;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (ReferenceEquals(obj, null) || obj.GetType() != GetType())
            {
                return false;
            }

            Vector v = (Vector)obj;

            if (coordinates.Length != v.coordinates.Length)
            {
                return false;
            }

            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i] != v.coordinates[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int prime = 47;
            int hash = 1;

            foreach (var e in coordinates)
            {
                hash = prime * hash + e.GetHashCode();
            }

            return hash;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("{ ").Append(string.Join(", ", coordinates)).Append(" }");

            return stringBuilder.ToString();
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector(vector1).Add(vector2);
        }

        public static Vector Subtract(Vector vector1, Vector vector2)
        {
            return new Vector(vector1).Subtract(vector2);
        }

        public static double GetScalarProduct(Vector vector1, Vector vector2)
        {
            double result = 0;

            int minSize = Math.Min(vector1.coordinates.Length, vector2.coordinates.Length);

            for (int i = 0; i < minSize; i++)
            {
                result += vector1.coordinates[i] * vector2.coordinates[i];
            }

            return result;
        }
    }
}
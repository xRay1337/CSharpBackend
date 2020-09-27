using System;
using System.Linq;

namespace Vectors
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] testArray = { 1, 2, 3 };

            Console.WriteLine("Конструкторы:");
            Console.WriteLine();

            Vector vector1 = new Vector(5);
            Console.WriteLine("Конструктор по размеру: " + vector1);

            Vector vector2 = new Vector(vector1);
            Console.WriteLine("Конструктор копирования: " + vector2);

            Vector vector3 = new Vector(testArray);
            Console.WriteLine("Конструктор с массивом: " + vector3);

            Vector vector4 = new Vector(5, testArray);
            Console.WriteLine("Конструктор с массивом и размером 5: " + vector4);

            Console.WriteLine();
            Console.WriteLine("Нестатические методы:");
            Console.WriteLine();

            Console.WriteLine("Размерность первого вектора: {0}", vector1);
            Vector vector5 = new Vector(vector3);
            Console.WriteLine("Сумма векторов {0} и {1} равна {2}", vector5, vector4, vector3.Add(vector4));
            vector5 = new Vector(vector3);
            Console.WriteLine("Разность векторов {0} и {1} равна {2}", vector5, vector4, vector3.Subtract(vector4));
            vector5 = new Vector(vector3);
            Console.WriteLine("Умножение вектора {0} на скаляр {1} равно {2}", vector5, 5, vector3.Multiply(5));
            vector5 = new Vector(vector3);
            Console.WriteLine("Разворот вектора {0} равен {1}", vector5, vector3.Reverse());
            Console.WriteLine("Длина вектора {0} равна {1:f2}", vector3, vector3.GetLength());
            Console.WriteLine("X вектора {0} равен {1}", vector3, vector3.GetElement(0));
            vector5 = new Vector(vector3);
            Console.WriteLine("Меняем X вектора {0} на {1}, сейчас вектор выглядит так: {2}", vector5, 25, vector3.SetElement(0, 25));

            Console.WriteLine("Метод Equals для {0} и {1} возвращает: {2}", vector3, vector4, vector3.Equals(null));
            Console.WriteLine("Хэш-кодом для вектора {0} является: {1}", vector3, vector3.GetHashCode());

            Console.WriteLine();
            Console.WriteLine("Статические методы:");
            Console.WriteLine();

            Console.WriteLine("Сумма векторов {0} и {1} равна {2}", vector3, vector4, Vector.Add(vector3, vector4));
            Console.WriteLine("Разность векторов {0} и {1} равна {2}", vector3, vector4, Vector.Subtract(vector3, vector4));
            Console.WriteLine("Скалярное произведение векторов {0} и {1} равно {2}", vector3, vector4, Vector.GetScalarProduct(vector3, vector4));

            Console.ReadKey();
        }
    }
}
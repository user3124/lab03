using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SquareMatrix matrix1 = null;
            SquareMatrix matrix2 = null;

            try
            {
                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("=== Матричный калькулятор ===");
                    Console.WriteLine("[1] Сгенерировать случайные матрицы");
                    Console.WriteLine("[2] Показать матрицы");
                    Console.WriteLine("[3] Сложить матрицы");
                    Console.WriteLine("[4] Перемножить матрицы");
                    Console.WriteLine("[5] Сравнить матрицы (>, <, ==, !=)");
                    Console.WriteLine("[6] Проверить операторы true / false");
                    Console.WriteLine("[7] Найти детерминант");
                    Console.WriteLine("[8] Получить обратную матрицу");
                    Console.WriteLine("[9] Клонировать матрицу (shallow / deep)");
                    Console.WriteLine("[0] Сравнение с помощью CompareTo");
                    Console.WriteLine("[Q] Выход");
                    Console.Write("Выберите пункт меню: ");

                    string choice = Console.ReadLine();
                    Console.WriteLine();

                    switch (choice.ToUpper())
                    {
                        case "1":
                            Console.Write("Введите размер квадратной матрицы: ");
                            int size = int.Parse(Console.ReadLine());
                            matrix1 = new SquareMatrix(size);
                            matrix2 = new SquareMatrix(size);
                            Console.WriteLine("Матрицы сгенерированы.");
                            break;

                        case "2":
                            Console.WriteLine("Матрица 1:");
                            Console.WriteLine(matrix1);
                            Console.WriteLine("Матрица 2:");
                            Console.WriteLine(matrix2);
                            break;

                        case "3":
                            Console.WriteLine("Результат сложения:");
                            Console.WriteLine(matrix1 + matrix2);
                            break;

                        case "4":
                            Console.WriteLine("Результат умножения:");
                            Console.WriteLine(matrix1 * matrix2);
                            break;

                        case "5":
                            Console.WriteLine($"matrix1 > matrix2: {matrix1 > matrix2}");
                            Console.WriteLine($"matrix1 < matrix2: {matrix1 < matrix2}");
                            Console.WriteLine($"matrix1 == matrix2: {matrix1 == matrix2}");
                            Console.WriteLine($"matrix1 != matrix2: {matrix1 != matrix2}");
                            break;

                        case "6":
                            Console.WriteLine($"matrix1 -> {(matrix1 ? "true" : "false")}");
                            Console.WriteLine($"matrix2 -> {(matrix2 ? "true" : "false")}");
                            break;

                        case "7":
                            Console.WriteLine($"Детерминант matrix1: {matrix1.Determinant()}");
                            Console.WriteLine($"Детерминант matrix2: {matrix2.Determinant()}");
                            break;

                        case "8":
                            Console.WriteLine("Обратная матрица для matrix1:");
                            var inverse = matrix1.Inverse();
                            Console.WriteLine(inverse);
                            break;

                        case "9":
                            var shallow = (SquareMatrix)matrix1.ShallowClone();
                            var deep = (SquareMatrix)matrix1.Clone();

                            Console.WriteLine("Shallow Сlone (тот же массив):");
                            Console.WriteLine(shallow);
                            Console.WriteLine("Ссылается на тот же массив: " + Object.ReferenceEquals(matrix1.Matrix, shallow.Matrix)); // 

                            Console.WriteLine("Deep Сlone (новый массив):");
                            Console.WriteLine(deep);
                            Console.WriteLine("Ссылается на тот же массив: " + Object.ReferenceEquals(matrix1.Matrix, deep.Matrix)); // false
                            break;

                        case "0":
                            int result = matrix1.CompareTo(matrix2);
                            if (result > 0)
                                Console.WriteLine("matrix1 больше matrix2");
                            else if (result < 0)
                                Console.WriteLine("matrix1 меньше matrix2");
                            else
                                Console.WriteLine("matrix1 равна matrix2");
                            break;

                        case "Q":
                            exit = true;
                            continue;

                        default:
                            Console.WriteLine("Неверный выбор, попробуйте снова.");
                            break;
                    }

                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введено нечисловое значение.");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Ошибка: {exception.Message}");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Произошло непредвиденное исключение: {exception.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}

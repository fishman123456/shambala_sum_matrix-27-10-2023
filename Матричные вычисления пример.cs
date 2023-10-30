using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsExample
{
    internal class Program
    {
        // вспомогательная функция для проверкли результата 
        static bool CheckResult(int[][]r1, int[][]r2)
        {
            for (int i = 0; i < r1.Length; i++)
            {
                for (int j = 0; j < r1[i].Length; j++)
                {
                    if (r1[i][j] != r2[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // метод генерации матрицы заданной размерности 
        // со случайными значениями в данном диапазоне
        // вход: m - число строк, n - число столбцов
        // min, max - диапазон случайных значений
        // выход: сгнерированную матрицу
        static int[][] GenerateMatrix(int m, int n, int min, int max, Random r)
        {
            int[][] matrix = new int[m][];
            for (int i = 0; i < m; i++)
            {
                matrix[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    matrix[i][j] = r.Next(min, max + 1);
                }
            }
            return matrix;
        }

        // метод вывода матрицы в консоль
        static void PrintMatrix(int[][] matrix)
        {
            foreach (int[] row in matrix)
            {
                foreach (int item in row)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
            }
        }

        // однопоточное сложение двух матриц
        // вход: 2 матрицы m1, m2
        // выход: результат сложения двух матриц - новая матрица
        static int[][] SumMatrix(int[][] m1, int[][] m2)
        {
            int[][] result = new int[m1.Length][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new int[m1[i].Length];
                for (int j = 0; j < m1[i].Length; j++)
                {
                    result[i][j] = m1[i][j] + m2[i][j];
                }
            }
            return result;
        }

        // многопоточное сложение двух матриц
        // вход: матрицы m1, m2; n - кол-во потоков
        // выход: результат сложения двух матриц - новая матрица
        static int[][] SumMatrix(int[][] m1, int[][] m2, int n)
        {
            // 1. ПОДГОТОВКА ПОТОКОВ
            // будем использовать массив потоков
            Thread[] threads = new Thread[n];
            // массив результата
            int[][] result = new int[m1.Length][];
            // m / n - кол-во строк, обрабатываемых одним потоком
            int step = m1.Length / n;   
            // необходимо запустить n-потоков в обработку
            // в цикле запускаем n-1 поток
            for (int k = 0; k < n; k++)
            {
                int start = k * step;   // индекс первой строки, обрабатываемой потоком
                int end = start + step; // индекс последней строки, обрабатываемой потоком
                if (k == n - 1)
                {
                    end = m1.Length;    // последний поток отрабатывает до конца
                }
                threads[k] = new Thread(() =>
                {
                    // цикл сложения строк
                    for (int i = start; i < end; i++)
                    {
                        result[i] = new int[m1[i].Length];
                        for (int j = 0; j < m1[i].Length; j++)
                        {
                            result[i][j] = m1[i][j] + m2[i][j];
                        }
                    }
                });
            }

            // 2. ЗАПУСК ПОТОКОВ
            foreach (Thread t in threads)
            {
                t.Start();
            }

            // 3. ДОЖДАТЬСЯ ЗАВЕРШЕНИЯ РАБОТЫ
            foreach (Thread t in threads)
            {
                t.Join();
            }

            // 4. вернуть результат
            return result;
        }

        static void Main(string[] args)
        {
            Console.Write("Press enter to continue ...");
            Console.ReadLine();

            Random r = new Random();
            int m = 10000, n = 10000, min = 1, max = 9;
            int[][] m1 = GenerateMatrix(m, n, min, max, r);
            Console.WriteLine();
            int[][] m2 = GenerateMatrix(m, n, min, max, r);

         
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine("Начало отсчета");
            int[][] m3 = SumMatrix(m1, m2, 1);
            Console.WriteLine($"Время работы: {sw.ElapsedMilliseconds} ms.");

            Console.WriteLine($"Проверка: {CheckResult(m3, SumMatrix(m1, m2))}");
        }
    }
}

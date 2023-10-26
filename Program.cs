using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shambala_sum_matrix_27_10_2023
{
    internal class Program
    {
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
            for (int i = 0;i < m1.Length;i++)
            {
                result[i] = new int[m1[i].Length];
                    for (int j = 0;j < m1[i].Length;j++)
                {
                    result[i][j] = m1[i][j] + m2[i][j];
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            Random r = new Random();
            int m = 3, n = 3, min = 1, max = 9;
            int[][] m1 = GenerateMatrix(m, n, min, max, r);
            PrintMatrix(m1);
            Console.WriteLine();
            int[][] m2 = GenerateMatrix(m, n, min, max, r);
            PrintMatrix(m2);
            Console.WriteLine();

            // 
            int[][] m3 = SumMatrix(m1, m2);
            PrintMatrix(m3);
            Console.ReadLine();
        }
    }
}

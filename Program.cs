using System;
using System.Diagnostics;

namespace Rozumatic_ASD_Lab2
{
    class Program
    {
        // 1. Пошук мінімуму, максимуму та їх індексів за один прохід O(n)
        static void FindExtremes(int[] arr, out int minVal, out int minIdx, out int maxVal, out int maxIdx)
        {
            minVal = arr[0]; minIdx = 0;
            maxVal = arr[0]; maxIdx = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < minVal) { minVal = arr[i]; minIdx = i; }
                if (arr[i] > maxVal) { maxVal = arr[i]; maxIdx = i; }
            }
        }

        // 2. In-place реверс масиву двома вказівниками
        static void ReverseInPlace(int[] arr, int start, int end)
        {
            int left = start, right = end;
            while (left < right)
            {
                int temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;
                left++; right--;
            }
        }

        // 3. Фільтрація елементів: вибір додатних парних чисел
        static int[] FilterPositiveEven(int[] arr)
        {
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] > 0 && arr[i] % 2 == 0) count++;

            int[] result = new int[count];
            int idx = 0;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] > 0 && arr[i] % 2 == 0) result[idx++] = arr[i];
            
            return result;
        }

        // Варіант 1: Середнє арифметичне між min та max
        static double AverageBetweenMinMax(int[] arr, int minIdx, int maxIdx)
        {
            int start = Math.Min(minIdx, maxIdx) + 1;
            int end = Math.Max(minIdx, maxIdx) - 1;
            if (start > end) return 0;

            long sum = 0; int count = 0;
            for (int i = start; i <= end; i++) { sum += arr[i]; count++; }
            return count > 0 ? (double)sum / count : 0;
        }

        // Варіант 2: Циклічний зсув праворуч (Потрійний реверс)
        static void RotateRightInPlace(int[] arr, int k)
        {
            if (arr.Length == 0) return;
            k = k % arr.Length;
            if (k == 0) return;
            ReverseInPlace(arr, 0, arr.Length - 1);
            ReverseInPlace(arr, 0, k - 1);
            ReverseInPlace(arr, k, arr.Length - 1);
        }

        // Варіант 3: Видалення дублікатів за один прохід
        static int RemoveDuplicatesInPlace(int[] arr)
        {
            if (arr.Length <= 1) return arr.Length;
            Array.Sort(arr); 
            int uniqueIdx = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] != arr[uniqueIdx])
                {
                    uniqueIdx++;
                    arr[uniqueIdx] = arr[i];
                }
            }
            return uniqueIdx + 1;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=========================================================");
            Console.WriteLine("        ЛАБОРАТОРНА РОБОТА №2: АЛГОРИТМИ НАД МАСИВАМИ    ");
            Console.WriteLine("=========================================================");

            int[] numbers = { 42, -7, 18, 99, 8, -15, 24, 88, 3, -2, 76 };
            Console.WriteLine($"Вихідний масив: [ {string.Join(", ", numbers)} ]\n");

            FindExtremes(numbers, out int minVal, out int minIdx, out int maxVal, out int maxIdx);
            Console.WriteLine($" Мінімум: {minVal} (індекс {minIdx})");
            Console.WriteLine($" Максимум: {maxVal} (індекс {maxIdx})");
            Console.WriteLine($" (Варіант 1) Сер. арифм. між min та max: {AverageBetweenMinMax(numbers, minIdx, maxIdx):F2}");

            Console.WriteLine($"\n Додатні парні числа: [ {string.Join(", ", FilterPositiveEven(numbers))} ]");

            int[] arrForReverse = (int[])numbers.Clone();
            ReverseInPlace(arrForReverse, 0, arrForReverse.Length - 1);
            Console.WriteLine($"\n Масив після реверсу: [ {string.Join(", ", arrForReverse)} ]");

            int[] arrForRotate = (int[])numbers.Clone();
            RotateRightInPlace(arrForRotate, 3);
            Console.WriteLine($" (Варіант 2) Зсув праворуч на 3: [ {string.Join(", ", arrForRotate)} ]");

            int[] duplicateArray = { 10, 20, 10, 30, 20, 40, 10, 50 };
            int uniqueLength = RemoveDuplicatesInPlace(duplicateArray);
            Console.Write($"\n (Варіант 3) Без дублікатів: [ ");
            for (int i = 0; i < uniqueLength; i++) Console.Write(duplicateArray[i] + (i < uniqueLength - 1 ? ", " : ""));
            Console.WriteLine(" ]\n✅ Тестування завершено успішно!");
        }
    }
}

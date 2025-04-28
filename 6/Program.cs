using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class SortingVerifier
{
    delegate void SortingMethod(int[] array);

    static void Main()
    {
        string filePath = "testcases.txt";

        int[][] testCases = ReadTestCasesFromFile(filePath);

        SortingMethod selectionSort = SelectionSort;
        SortingMethod shakerSort = ShakerSort;
        SortingMethod studentSelectionSort = StudentSelectionSort;
        SortingMethod studentShakerSort = StudentShakerSort;

        foreach (var testCase in testCases)
        {
            Console.WriteLine("Тестування масиву: " + string.Join(", ", testCase));

            Console.WriteLine("\nSelectionSort vs StudentSelectionSort:");
            CompareSorts(testCase, selectionSort, studentSelectionSort);

            Console.WriteLine("\nShakerSort vs StudentShakerSort:");
            CompareSorts(testCase, shakerSort, studentShakerSort);

            Console.WriteLine(new string('-', 40));
        }
    }

    static int[][] ReadTestCasesFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        List<int[]> testCases = new List<int[]>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            int[] numbers = line.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();
            testCases.Add(numbers);
        }

        return testCases.ToArray();
    }

    static void CompareSorts(int[] originalArray, SortingMethod standardSort, SortingMethod studentSort)
{
    int[] standardArray = (int[])originalArray.Clone();
    int[] studentArray = (int[])originalArray.Clone();

    double standardTime = MeasureTime(standardSort, standardArray);
    double studentTime = MeasureTime(studentSort, studentArray);

    bool isCorrect = standardArray.SequenceEqual(studentArray);

    Console.WriteLine($"Правильність: {(isCorrect ? "Правильно" : "Неправильно")}");
    Console.WriteLine($"Час стандартного сортування: {standardTime:F3} мкс");
    Console.WriteLine($"Час студентського сортування: {studentTime:F3} мкс");

    double difference = studentTime - standardTime;
    Console.WriteLine($"Різниця часу: {difference:F3} мс");
}


    static long MeasureTime(SortingMethod sortMethod, int[] array)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        sortMethod(array);
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    static void SelectionSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < array[minIndex])
                    minIndex = j;
            }
            (array[i], array[minIndex]) = (array[minIndex], array[i]);
        }
    }

    static void ShakerSort(int[] array)
    {
        bool swapped;
        int start = 0;
        int end = array.Length - 1;

        do
        {
            swapped = false;
            for (int i = start; i < end; i++)
            {
                if (array[i] > array[i + 1])
                {
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    swapped = true;
                }
            }
            if (!swapped) break;
            swapped = false;
            end--;

            for (int i = end; i > start; i--)
            {
                if (array[i - 1] > array[i])
                {
                    (array[i], array[i - 1]) = (array[i - 1], array[i]);
                    swapped = true;
                }
            }
            start++;
        } while (swapped);
    }

    static void StudentSelectionSort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int min = array[i];
            int minPos = i;
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < min)
                {
                    min = array[j];
                    minPos = j;
                }
            }
            int temp = array[i];
            array[i] = array[minPos];
            array[minPos] = temp;
        }
    }

    static void StudentShakerSort(int[] array)
    {
        int left = 0;
        int right = array.Length - 1;
        bool swapped = true;

        while (swapped)
        {
            swapped = false;
            for (int i = left; i < right; i++)
            {
                if (array[i] > array[i + 1])
                {
                    int temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                    swapped = true;
                }
            }
            right--;

            for (int i = right; i > left; i--)
            {
                if (array[i - 1] > array[i])
                {
                    int temp = array[i];
                    array[i] = array[i - 1];
                    array[i - 1] = temp;
                    swapped = true;
                }
            }
            left++;
        }
    }
}

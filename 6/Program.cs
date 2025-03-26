using System;
using System.Diagnostics;

class SortingVerifier
{
    delegate void SortingMethod(int[] array);

    static void Main()
    {
        int[][] testCases =
        {
            new int[] { 5, 3, 8, 4, 2 },
            new int[] { 10, 20, 5, 3, 7, 2, 11 },
            new int[] { 1, 2, 3, 4, 5 }
        };

        SortingMethod selectionSort = SelectionSort;
        SortingMethod shakerSort = ShakerSort;
        SortingMethod studentSort1 = StudentSort1;
        SortingMethod studentSort2 = StudentSort2;

        foreach (var testCase in testCases)
        {
            Console.WriteLine("Тестування масиву: " + string.Join(", ", testCase));

            ValidateSorting(testCase, selectionSort, studentSort1, "StudentSort1");
            ValidateSorting(testCase, selectionSort, studentSort2, "StudentSort2");
            Console.WriteLine();
        }
    }

    static void SelectionSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < array.Length; j++)
                if (array[j] < array[minIndex])
                    minIndex = j;
            
            (array[i], array[minIndex]) = (array[minIndex], array[i]);
        }
    }

    static void ShakerSort(int[] array)
    {
        bool swapped;
        do
        {
            swapped = false;
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    swapped = true;
                }
            }
            
            if (!swapped) break;
            
            swapped = false;
            for (int i = array.Length - 2; i >= 0; i--)
            {
                if (array[i] > array[i + 1])
                {
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    swapped = true;
                }
            }
        } while (swapped);
    }

    static void StudentSort1(int[] array)
    {
        Array.Sort(array); 
    }

    static void StudentSort2(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;
            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    static void ValidateSorting(int[] testCase, SortingMethod referenceSort, SortingMethod studentSort, string methodName)
    {
        int[] reference = (int[])testCase.Clone();
        referenceSort(reference);

        int[] student = (int[])testCase.Clone();
        studentSort(student);

        if (reference.Length != student.Length || !reference.SequenceEqual(student))
            Console.WriteLine($"{methodName} Працює неправильно.");
        else
            Console.WriteLine($"{methodName} Працює правильно.");
    }
}

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
/*Використовуючи делегати, написати програму, яка із заданого масиву int[] формує
 новий масив int[] з лише тими елементами початкового масиву, які кратні
 деякому k (ціле число, що вводиться на початку).*/

class Program
{
    delegate bool MultipleDelegate(int number, int k);
    
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.Write("Введіть число k: ");
        if (!int.TryParse(Console.ReadLine(), out int k))
        {
            Console.WriteLine("Некоректне введення!");  
            return;         
        }
         
        int[] startArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 15, 18, 20 };

        MultipleDelegate isMultiple = IsMultiple;

        int[] filteredLinq = startArray.Where(x => isMultiple(x, k)).ToArray();
        Console.WriteLine("Результат через LINQ: " + string.Join(", ", filteredLinq));

        int[] filteredManual = FilterArray(startArray, isMultiple, k);
        Console.WriteLine("Результат власної реалізації: " + string.Join(", ", filteredManual));
    }
    static bool IsMultiple(int x, int k)
    {
        return x % k == 0;
    }

    static int[] FilterArray(int[] input, MultipleDelegate isMultiple, int k)
    {
        int count = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (isMultiple(input[i], k))
            {
                count++;
            }
        }

        int[] finalArray = new int[count];
        int index = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (isMultiple(input[i], k))
            {
                finalArray[index++] = input[i];
            }
        }

        return finalArray;
    }
}

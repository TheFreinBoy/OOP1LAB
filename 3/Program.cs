using System;
using System.Text;

class Program
{
    public delegate double SeriesTerm(int n);
    public static double ComputeSeriesSum(SeriesTerm term, double epsilon)
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        double sum = 0;
        double currentTerm;
        int n = 0;

        do
        {
            currentTerm = term(n);
            sum += currentTerm;
            n++;
        } while (Math.Abs(currentTerm) > epsilon);

        return sum;
    }

    static void Main()
    {
        double epsilon = 0.0001; 

        // 1 + 1/2 + 1/4 + 1/8 
        SeriesTerm series1 = n => 1.0 / Math.Pow(2, n);
        Console.WriteLine($"Сума першого ряду: {ComputeSeriesSum(series1, epsilon)}");

        // 1 + 1/2! + 1/3! + 1/4! 
        SeriesTerm series2 = n => 1.0 / Factorial(n + 1);
        Console.WriteLine($"Сума другого ряду: {ComputeSeriesSum(series2, epsilon)}");

        //-1 + 1/2 - 1/4 + 1/8 - 1/16 
        SeriesTerm series3 = n => Math.Pow(-1, n) / Math.Pow(2, n);
        Console.WriteLine($"Сума третього ряду: {ComputeSeriesSum(series3, epsilon)}");
    }

    public static double Factorial(int n)
    {
        if (n == 0 || n == 1) return 1;
        double result = 1;
        for (int i = 2; i <= n; i++)
            result *= i;
        return result;
    }
}

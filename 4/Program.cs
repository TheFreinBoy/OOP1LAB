using System;
using System.Text;

namespace Da
{
	class Program
	{
		private static void Text()
		{		
			Console.WriteLine("\t0 -- sqrt(abs(x))");
			Console.WriteLine("\t1 -- x^3 (x*x*x)");
			Console.WriteLine("\t2 -- x + {0}", 3.5);
			Console.WriteLine("\nЩоб завершити програму натисніть щось, що не задовольняє умову.\n");
		}

		private static void Main(string[] args)
		{
            Console.OutputEncoding = UTF8Encoding.UTF8;
			Func<double, double>[] array = new Func<double, double>[3];
			array[0] = ((double x) => Math.Sqrt(Math.Abs(x)));
			array[1] = ((double x) => Math.Pow(x, 3.0));
			array[2] = ((double x) => x + 3.5);
			Func<double, double>[] array2 = array;
			Program.Text();
			try
			{
				for (;;)
				{
					string[] input = Console.ReadLine().Trim().Split();
                    int index = int.Parse(input[0]);
                    double value = double.Parse(input[1]);
                    Console.WriteLine(array[index](value));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Сталася помилка {0}", ex.Message);
				Console.WriteLine("Натисніть будь-яку клавішу для остаточного виходу");
				Console.ReadKey();
			}
		}

	}
}
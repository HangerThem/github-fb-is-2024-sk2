using System;
using System.Diagnostics;

namespace program_008_other_sorting_algorithms {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Enter n, top and bottom:");
			int n = int.Parse(Console.ReadLine() ?? "0");
			int top = int.Parse(Console.ReadLine() ?? "0");
			int bottom = int.Parse(Console.ReadLine() ?? "0");
			
			int[] numbers = new int[n];
			Random random = new();

			Console.WriteLine("Before sorting:");
			for (int i = 0; i < n; i++) {
				numbers[i] = random.Next(bottom, top);
				Console.Write(numbers[i] + " ");
			}
			Console.WriteLine();

			TimeAndSort("SelectionSort", numbers, SelectionSort.Sort);
			TimeAndSort("InsertionSort", numbers, InsertionSort.Sort);
			TimeAndSort("ShakerSort", numbers, ShakerSort.Sort);
			TimeAndSort("CombSort", numbers, CombSort.Sort);
			TimeAndSort("ShellSort", numbers, ShellSort.Sort);
		}

		static void TimeAndSort(string sortName, int[] numbers, Action<int[]> sortMethod) {
			int[] numbersCopy = (int[])numbers.Clone();
			Stopwatch stopwatch = Stopwatch.StartNew();
			sortMethod(numbersCopy);
			stopwatch.Stop();
			Console.WriteLine($"{sortName} took {stopwatch.ElapsedMilliseconds} ms");
			Console.WriteLine("After sorting:");
			foreach (var num in numbersCopy) {
				Console.Write(num + " ");
			}
			Console.WriteLine();
		}
	}
}
namespace program_005_min_a_max {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Enter n, top and bottom:");
			int n = int.Parse(Console.ReadLine());
			int top = int.Parse(Console.ReadLine());
			int bottom = int.Parse(Console.ReadLine());
			
			int[] numbers = new int[n];
			Random random = new();

			for (int i = 0; i < n; i++) {
				numbers[i] = random.Next(bottom, top);
			}

			int max = numbers[0];
			List<int> maxIndexes = [];
			int min = numbers[0];
			List<int> minIndexes = [];

			for (int i = 1; i < n; i++) {
				if (numbers[i] > max) {
					max = numbers[i];
					maxIndexes.Clear();
					maxIndexes.Add(i);
				} else if (numbers[i] == max) {
					maxIndexes.Add(i);
				}

				if (numbers[i] < min) {
					min = numbers[i];
					minIndexes.Clear();
					minIndexes.Add(i);
				} else if (numbers[i] == min) {
					minIndexes.Add(i);
				}
			}

			

			Console.WriteLine("Numbers:");
			foreach (int number in numbers) {
				Console.Write(number + " ");
			}
			Console.WriteLine();
			Console.WriteLine("Max: " + max);
			Console.WriteLine("Max index(es):");
			foreach (int index in maxIndexes) {
				Console.Write(index + " ");
			}
			Console.WriteLine();
			Console.WriteLine("Min: " + min);
			Console.WriteLine("Min index(es):");
			foreach (int index in minIndexes) {
				Console.Write(index + " ");
			}
			Console.WriteLine();
		}
	}
}
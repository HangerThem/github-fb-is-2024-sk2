namespace program_009_array_reversion {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Enter n, top and bottom:");
			int n = int.Parse(Console.ReadLine() ?? "0");
			int top = int.Parse(Console.ReadLine() ?? "0");
			int bottom = int.Parse(Console.ReadLine() ?? "0");
			
			int[] numbers = new int[n];
			Random random = new();

			Console.WriteLine("Before reversion:");
			for (int i = 0; i < n; i++) {
				numbers[i] = random.Next(bottom, top);
				Console.Write(numbers[i] + " ");
			}

			Revert(numbers);
		}

		static void Revert(int[] numbers) {
			int n = numbers.Length;
			for (int i = 0; i < n / 2; i++) {
				int temp = numbers[i];
				numbers[i] = numbers[n - i - 1];
				numbers[n - i - 1] = temp;
			}

			Console.WriteLine("\nAfter reversion:");
			foreach (var num in numbers) {
				Console.Write(num + " ");
			}

			Console.WriteLine();
		}
	}
}
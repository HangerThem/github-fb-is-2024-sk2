namespace program_004_pseudorandom_number_generator {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Enter n, top and bottom:");
			int n = int.Parse(Console.ReadLine());
			int top = int.Parse(Console.ReadLine());
			int bottom = int.Parse(Console.ReadLine());
			
			int[] numbers = new int[n];
			Random random = new();

			int negative = 0;
			int positive = 0;
			int zero = 0;
			int odd = 0;
			int even = 0;

			for (int i = 0; i < n; i++) {
				numbers[i] = random.Next(bottom, top);
				if (numbers[i] < 0) {
					negative++;
				} else if (numbers[i] > 0) {
					positive++;
				} else {
					zero++;
				}

				if (numbers[i] % 2 == 0) {
					even++;
				} else {
					odd++;
				}
			}

			Console.WriteLine("Negative: " + negative);
			Console.WriteLine("Positive: " + positive);
			Console.WriteLine("Zero: " + zero);
			Console.WriteLine("Odd: " + odd);
			Console.WriteLine("Even: " + even);
		}
	}
}
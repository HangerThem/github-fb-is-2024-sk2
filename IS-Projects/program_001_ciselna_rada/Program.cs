using System;

namespace tul_002 {
	class Program {
		private static bool runAgain = true;

		static void Main(string[] args) {
			while(runAgain) {
				int numberFrom = GetValidInput("Enter the number from:", input => int.TryParse(input, out _));
				int numberTo = GetValidInput("Enter the number to:", input => int.TryParse(input, out _));
				int numberStep = GetValidInput("Enter the number step:", input => int.TryParse(input, out int result) && result > 0);

				Console.WriteLine($"Numbers from {numberFrom} to {numberTo} with step {numberStep}:");
				for(int i = numberFrom; i <= numberTo; i += numberStep) {
					Console.WriteLine(i);
				}

				Console.WriteLine("Do you want to run again? (y/n)");
				runAgain = (Console.ReadLine() ?? string.Empty).Equals("y", StringComparison.CurrentCultureIgnoreCase);
				Console.Clear();
			}
		}

		private static int GetValidInput(string prompt, Func<string, bool> validate) {
			while (true) {
				Console.WriteLine(prompt);
				string input = Console.ReadLine() ?? string.Empty;
				if (validate(input) && int.TryParse(input, out int result)) {
					return result;
				}
				Console.WriteLine("Invalid input. Please enter a valid integer.");
			}
		}
	}
}
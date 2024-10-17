using System;

namespace program_002_soucet_cifer {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Do you want classic mathematical addition or another style?");
			Console.WriteLine("1. Mathematical");
			Console.WriteLine("2. Another style");

			int choice;
			int number;

			while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
			{
				Console.WriteLine("Invalid input. Please try again.");
			}

			Console.WriteLine("Enter the number:");
			while (!int.TryParse(Console.ReadLine(), out number))
			{
				Console.WriteLine("Invalid input. Please try again.");
			}

			if (choice == 1) {
				Console.WriteLine("The sum of the digits (mathematical) is: " + SumDigitsMathematically(number));
			}
			else {
				Console.WriteLine("The sum of the digits (alternative) is: " + SumDigitsByString(number));
			}
		}

		static int SumDigitsMathematically(int num) {
			int sum = 0;
			while (num != 0) {
				sum += num % 10;
				num /= 10;
			}
			return sum;
		}

		static int SumDigitsByString(int num) {
			string numStr = Math.Abs(num).ToString();
			int sum = 0;
			foreach (char digit in numStr) {
				sum += int.Parse(digit.ToString());
			}
			return sum;
		}
	}
}

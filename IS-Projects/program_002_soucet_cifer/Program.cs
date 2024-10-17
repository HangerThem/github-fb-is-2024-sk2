using System;

namespace program_002_soucet_cifer {
	class Program {
		static void Main(string[] args) {
			int choice1;
			int choice2;
			int number;
			bool again = true;

			while(again) {
				again = false;
				Console.WriteLine("Do you want classic mathematical addition/multiplication or another style?");
				Console.WriteLine("1. Mathematical");
				Console.WriteLine("2. Another style");

				while (!int.TryParse(Console.ReadLine(), out choice1) || choice1 < 1 || choice1 > 2)
				{
					Console.WriteLine("Invalid input. Please try again.");
				}

				Console.WriteLine("Do you want to sum or multiply the digits?");
				Console.WriteLine("1. Sum");
				Console.WriteLine("2. Multiply");
				while (!int.TryParse(Console.ReadLine(), out choice2) || choice2 < 1 || choice2 > 2)
				{
					Console.WriteLine("Invalid input. Please try again.");
				}

				Console.WriteLine("Enter the number:");
				while (!int.TryParse(Console.ReadLine(), out number))
				{
					Console.WriteLine("Invalid input. Please try again.");
				}

				if (choice1 == 1) {
					if (choice2 == 1) {
						Console.WriteLine(SumDigitsMathematically(number));
					} else {
						Console.WriteLine(MultiplyDigitsMathematically(number));
					}
				} else {
					if (choice2 == 1) {
						Console.WriteLine(SumDigitsByString(number));
					} else {
						Console.WriteLine(MultiplyDigitsByString(number));
					}
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

			static int MultiplyDigitsMathematically(int num) {
				int product = 1;
				while (num != 0) {
					product *= num % 10;
					num /= 10;
				}
				return product;
			}

			static int MultiplyDigitsByString(int num) {
				string numStr = Math.Abs(num).ToString();
				int product = 1;
				foreach (char digit in numStr) {
					product *= int.Parse(digit.ToString());
				}
				return product;
			}

			Console.WriteLine("Do you want to try again? (y/n)");
			string answer = Console.ReadLine();
			if (answer == "y") {
				again = true;
			}
		}
	}
}

using System;

namespace program_003_vykresleni_obrazcu {
	class Program {
		static void Main() {
			while (true) {
				Console.WriteLine("Shape Drawing");
				Console.WriteLine("1. Rectangle");
				Console.WriteLine("2. Triangle");
				Console.WriteLine("3. Tree");
				Console.WriteLine("4. Sphere");
				Console.WriteLine("5. Exit");
				Console.Write("Choose: ");
				if (!int.TryParse(Console.ReadLine(), out int choice)) {
					Console.WriteLine("Invalid choice");
					continue;
				}

				switch (choice) {
					case 1:
						DrawRectangle();
						break;
					case 2:
						DrawTriangle();
						break;
					case 3:
						DrawTree();
						break;
					case 4:
						DrawSphere();
						break;
					case 5:
						Console.WriteLine("Exit");
						return;
					default:
						Console.WriteLine("Invalid choice");
						break;
				}
			}
		}

		static void DrawRectangle() {
			Console.Write("Enter width: ");
			if (!int.TryParse(Console.ReadLine(), out int width)) {
				Console.WriteLine("Invalid input");
				return;
			}
			Console.Write("Enter height: ");
			if (!int.TryParse(Console.ReadLine(), out int height)) {
				Console.WriteLine("Invalid input");
				return;
			}
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					Console.Write("*");
				}
				Console.WriteLine();
			}
		}

		static void DrawTriangle() {
			Console.Write("Enter height: ");
			if (!int.TryParse(Console.ReadLine(), out int height)) {
				Console.WriteLine("Invalid input");
				return;
			}
			for (int i = 0; i < height; i++) {
				for (int j = 0; j <= i; j++) {
					Console.Write("*");
				}
				Console.WriteLine();
			}
		}

		static void DrawTree() {
			Console.Write("Enter height: ");
			if (!int.TryParse(Console.ReadLine(), out int height)) {
				Console.WriteLine("Invalid input");
				return;
			}
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < height - i - 1; j++) {
					Console.Write(" ");
				}
				for (int j = 0; j < 2 * i + 1; j++) {
					Console.Write("*");
				}
				Console.WriteLine();
			}
		}

		static void DrawSphere() {
			Console.Write("Enter radius: ");
			if (!int.TryParse(Console.ReadLine(), out int radius)) {
				Console.WriteLine("Invalid input");
				return;
			}
			for (int i = -radius; i <= radius; i++) {
				for (int j = -radius; j <= radius; j++) {
					if (i * i + j * j <= radius * radius) {
						Console.Write("*");
					} else {
						Console.Write(" ");
					}
				}
				Console.WriteLine();
			}
		}
	}
}
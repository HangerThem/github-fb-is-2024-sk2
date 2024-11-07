namespace program_006_analyza_textu {
	class Program {
		static void Main(string[] args) {
			string choice = "";
			string input = "";
			string text = "";
			string path = "";
			bool file = false;

			Console.WriteLine("Input text manually or from file? (m/f)");
			while (choice != "m" && choice != "f") {
				choice = Console.ReadLine() ?? "";
				if (choice == "m") {
					Console.WriteLine("Input text:");
					input = Console.ReadLine() ?? "";
				} else if (choice == "f") {
					Console.WriteLine("Input file path:");
					path = Console.ReadLine() ?? "";
					file = true;
				} else {
					Console.WriteLine("Invalid input. Try again.");
				}
			}

			if (file) {
				try {
					text = File.ReadAllText(path);
				} catch (Exception e) {
					Console.WriteLine("Error reading file: " + e.Message);
				}
			} else {
				text = input;
			}

			Analyzer analyzer = new(text);
			analyzer.Analyze();
			analyzer.PrintResults();

			Console.ReadKey();
		}
	}
}
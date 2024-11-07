using System.Globalization;
using System.Text;
using System.Linq;

namespace program_006_analyza_textu {
	class Analyzer {
		private readonly string text;
		private int sentenceCount;
		private int wordCount;
		private int charCount;
		private int vowelCount;
		private int consonantCount;
		private int specialCharCount;
		private int digitCount;

		public Analyzer(string text) {
			this.text = NormalizeDiacritics(text);
		}

		public void Analyze() {
			sentenceCount = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
			wordCount = text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
			charCount = text.Length;
			vowelCount = text.Count(c => "aeiouAEIOU".Contains(c));
			consonantCount = text.Count(c => "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ".Contains(c));
			specialCharCount = text.Count(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
			digitCount = text.Count(c => char.IsDigit(c));
		}

		public void PrintResults() {
			Console.WriteLine($"Number of sentences: {sentenceCount}");
			Console.WriteLine($"Number of words: {wordCount}");
			Console.WriteLine($"Number of characters: {charCount}");
			Console.WriteLine($"Number of vowels: {vowelCount}");
			Console.WriteLine($"Number of consonants: {consonantCount}");
			Console.WriteLine($"Number of special characters: {specialCharCount}");
			Console.WriteLine($"Number of digits: {digitCount}");
		}

		private static string NormalizeDiacritics(string text) {
			var normalizedString = text.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder();

			foreach (var c in normalizedString) {
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark) {
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}
	}
}


namespace program_008_other_sorting_algorithms {
	public static class CombSort {
		public static void Sort(int[] array) {
			int gap = array.Length;
			bool swapped = true;

			while (gap != 1 || swapped) {
				gap = GetNextGap(gap);
				swapped = false;
				for (int i = 0; i < array.Length - gap; i++) {
					if (array[i] > array[i + gap]) {
						int temp = array[i];
						array[i] = array[i + gap];
						array[i + gap] = temp;
						swapped = true;
					}
				}
			}
		}

		private static int GetNextGap(int gap) {
			gap = (gap * 10) / 13;
			if (gap < 1) return 1;
			return gap;
		}
	}
}
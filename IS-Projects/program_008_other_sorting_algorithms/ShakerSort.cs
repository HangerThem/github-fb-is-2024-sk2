
namespace program_008_other_sorting_algorithms {
	public static class ShakerSort {
		public static void Sort(int[] array) {
			bool swapped = true;
			int start = 0;
			int end = array.Length;

			while (swapped) {
				swapped = false;
				for (int i = start; i < end - 1; ++i) {
					if (array[i] > array[i + 1]) {
						int temp = array[i];
						array[i] = array[i + 1];
						array[i + 1] = temp;
						swapped = true;
					}
				}
				if (!swapped) break;
				swapped = false;
				end--;
				for (int i = end - 1; i >= start; --i) {
					if (array[i] > array[i + 1]) {
						int temp = array[i];
						array[i] = array[i + 1];
						array[i + 1] = temp;
						swapped = true;
					}
				}
				start++;
			}
		}
	}
}

namespace program_008_other_sorting_algorithms {
	public static class InsertionSort {
		public static void Sort(int[] array) {
			int n = array.Length;
			for (int i = 1; i < n; i++) {
				int key = array[i];
				int j = i - 1;
				while (j >= 0 && array[j] > key) {
					array[j + 1] = array[j];
					j--;
				}
				array[j + 1] = key;
			}
		}
	}
}
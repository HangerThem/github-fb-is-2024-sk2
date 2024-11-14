
namespace program_008_other_sorting_algorithms {
	public static class ShellSort {
		public static void Sort(int[] array) {
			int n = array.Length;
			for (int gap = n / 2; gap > 0; gap /= 2) {
				for (int i = gap; i < n; i++) {
					int temp = array[i];
					int j;
					for (j = i; j >= gap && array[j - gap] > temp; j -= gap) {
						array[j] = array[j - gap];
					}
					array[j] = temp;
				}
			}
		}
	}
}
namespace program_008_other_sorting_algorithms {
	public static class SelectionSort {
		public static void Sort(int[] array) {
			int n = array.Length;
			for (int i = 0; i < n - 1; i++) {
				int minIndex = i;
				for (int j = i + 1; j < n; j++) {
					if (array[j] < array[minIndex]) {
						minIndex = j;
					}
				}
				int temp = array[minIndex];
				array[minIndex] = array[i];
				array[i] = temp;
			}
		}
	}
}
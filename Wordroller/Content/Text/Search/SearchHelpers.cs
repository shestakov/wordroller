using System;
using System.Collections.Generic;

namespace Wordroller.Content.Text.Search
{
	internal static class SearchHelpers
	{
		internal static int BinarySearch<T>(this T[] array, int targetIndex, int left, int right)
			where T : IndexedTextElement
		{
			if (left >= right) throw new ArgumentOutOfRangeException(nameof(right), right, "Segment cannot be empty");
			var center = (right + left) / 2;
			var value = array[center];
			var elementStartIndex = value.StartIndex;
			var elementEndIndex = elementStartIndex + value.Length;
			if (elementStartIndex <= targetIndex && targetIndex < elementEndIndex) return center;
			return targetIndex < elementStartIndex ? BinarySearch(array, targetIndex, left, center) : BinarySearch(array, targetIndex, center, right);
		}

		public static IEnumerable<int> AllIndexesOf(this string stringToSearch, string stringToSeek, StringComparison stringComparison)
		{
			if (string.IsNullOrEmpty(stringToSeek))
				throw new ArgumentException("The string sought for cannot be empty or null", nameof(stringToSeek));

			for (var index = 0;; index += stringToSeek.Length)
			{
				index = stringToSearch.IndexOf(stringToSeek, index, stringComparison);
				if (index == -1) break;
				yield return index;
			}
		}
	}
}
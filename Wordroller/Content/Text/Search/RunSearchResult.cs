using System.Collections.Generic;
using System.Linq;

namespace Wordroller.Content.Text.Search
{
	public class RunSearchResult
	{
		public RunSearchResult(int startIndex, int length, Run run)
		{
			StartIndex = startIndex;
			Length = length;
			Run = run;
			Elements = run.GetContentRange(startIndex, length);
		}
		public int StartIndex { get; }
		public int Length { get; }
		public Run Run { get; }
		public List<IndexedRunContentElement> Elements { get; }

		public IndexedRunContentElement[][] Remove()
		{
			return Elements.Select(element => element.Remove()).ToArray();
		}
	}
}
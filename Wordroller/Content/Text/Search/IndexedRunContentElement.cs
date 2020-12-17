using Wordroller.Content.Text.RunContent;

namespace Wordroller.Content.Text.Search
{
	public abstract class IndexedTextElement
	{
		protected IndexedTextElement(int startIndex, int length)
		{
			StartIndex = startIndex;
			Length = length;
		}

		public int StartIndex { get; }
		public int Length { get; }
	}

	public class IndexedRunContentElement : IndexedTextElement
	{
		public IndexedRunContentElement(RunContentElementBase elementBase, int startIndex, int length): base(startIndex, length)
		{
			ElementBase = elementBase;
		}

		public RunContentElementBase ElementBase { get; }

		public IndexedRunContentElement[] Remove()
		{
			if (ElementBase is RunText runText && runText.Text.Length != Length)
			{
				runText.RemoveSubstring(StartIndex, Length);
				return new[] {this};
			}

			ElementBase.Remove();
			return new IndexedRunContentElement[0];
		}
	}

	internal class IndexedRun: IndexedTextElement
	{
		public IndexedRun(Run run, int startIndex, int length): base(startIndex, length)
		{
			Run = run;
		}

		public Run Run { get; }
	}

}
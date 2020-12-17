namespace Wordroller.Content.Text.Search
{
	public class ParagraphSearchResults
	{
		public ParagraphSearchResults(Paragraph paragraph, int[] occurrences)
		{
			Paragraph = paragraph;
			Occurrences = occurrences;
		}

		public Paragraph Paragraph { get; }
		public int[] Occurrences { get; }
	}
}
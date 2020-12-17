using System;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.Search
{
	public class ParagraphSearchResult
	{
		private readonly DocumentContentContainer container;

		internal ParagraphSearchResult(int startIndex, int length, RunSearchResult[] runs, DocumentContentContainer container)
		{
			this.container = container;
			StartIndex = startIndex;
			Length = length;
			Runs = runs;
		}

		public int StartIndex { get; }
		public int Length { get; }
		public RunSearchResult[] Runs { get; }

		public void Remove()
		{
			foreach (var runSearchResult in Runs) runSearchResult.Remove();
		}

		public Run ReplaceWithTextRun(string text, bool keepFirstRunFormatting = false)
		{
			if (Runs.Length == 0) throw new Exception("Nothing found, nothing to replace");

			var replacement = Run.Create(text, container);

			if (keepFirstRunFormatting)
			{
				var rPr = Runs[0].Run.Xml.Element(Namespaces.w + "rPr");
				if (rPr != null) replacement.Xml.AddFirst(new XElement(rPr));
			}

			if (Runs.Length == 1) // We found entire text within a single run
			{
				var onlyFoundRun = Runs[0];
				var theOnlyRun = onlyFoundRun.Run;

				if (onlyFoundRun.StartIndex > 0 && onlyFoundRun.Length < onlyFoundRun.Run.Text.Length) // The content is strictly within the only run, keeping both tail and head
				{
					var tailRun = theOnlyRun.Cut(onlyFoundRun.StartIndex, onlyFoundRun.StartIndex + onlyFoundRun.Length);
					theOnlyRun.Xml.AddAfterSelf(tailRun.Xml);
					theOnlyRun.Xml.AddAfterSelf(replacement.Xml); // Adding the replacement BETWEEN the kept head and tail
				}
				else if (onlyFoundRun.StartIndex == 0) // The only run stars with the found text, must leave the tail
				{
					theOnlyRun.RemoveContentFromBeginning(onlyFoundRun.StartIndex + onlyFoundRun.Length);
					onlyFoundRun.Run.Xml.AddBeforeSelf(replacement.Xml); // Adding the replacement BEFORE the kept tail
				}
				else if (onlyFoundRun.Length == onlyFoundRun.Run.Text.Length) // The only run ends with the found text, must leave the head
				{
					theOnlyRun.RemoveContentStartingFrom(onlyFoundRun.StartIndex);
					onlyFoundRun.Run.Xml.AddAfterSelf(replacement.Xml); // Adding the replacement AFTER the kept head
				}

			}
			else // The found text is split between some successive runs
			{
				var firstFoundRun = Runs.First();
				var lastFoundRun = Runs.Last();

				var removeFirstRun = true;
				var removeLastRun = true;

				if (firstFoundRun.StartIndex > 0) // The found text DOES NOT start from the beginning of the first run, must leave the head
				{
					removeFirstRun = false;
					var firstRun = firstFoundRun.Run;
					firstRun.RemoveContentStartingFrom(firstFoundRun.StartIndex);
				}

				if (lastFoundRun.Length < lastFoundRun.Run.Text.Length) // The last run DOES NOT end with found text, must leave the tail
				{
					removeLastRun = false;
					var lastRun = lastFoundRun.Run;
					lastRun.RemoveContentFromBeginning(lastFoundRun.StartIndex + lastFoundRun.Length);
				}

				foreach (var foundRun in Runs)
				{
					if ((removeFirstRun || foundRun != firstFoundRun) && (removeLastRun || foundRun != lastFoundRun))
						foundRun.Remove();
				}

				if (removeFirstRun)
				{
					firstFoundRun.Run.Xml.ReplaceWith(replacement.Xml);
				}
				else
				{
					firstFoundRun.Run.Xml.AddAfterSelf(replacement.Xml);
				}
			}

			return replacement;
		}
	}
}
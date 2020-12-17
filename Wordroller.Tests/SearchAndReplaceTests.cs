using System;
using System.Linq;
using Wordroller.Content.Properties.Runs;
using Xunit;
using Xunit.Abstractions;

namespace Wordroller.Tests
{
	public class SearchAndReplaceTests : TestsBase
	{
		public SearchAndReplaceTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public void ReplacementInExistingDocument()
		{
			using var document = TestHelper.LoadDocumentFromResource("Wordroller.Tests.Resources.SearchAndReplace.docx");

			var results1 = document.Body.FindText("{{Placeholder1}}", StringComparison.InvariantCulture).ToArray();
			Assert.Equal(2, results1.Length);
			foreach (var result in results1) result.ReplaceWithTextRun("Replacement 1");

			var results2 = document.Body.FindText("{{Placeholder2}}", StringComparison.InvariantCulture).ToArray();
			Assert.Single(results2);
			foreach (var result in results2) result.ReplaceWithTextRun("Replacement 2", true);

			var results3 = document.Body.FindText("{{Placeholder3}}", StringComparison.InvariantCulture).ToArray();
			Assert.Single(results3);
			foreach (var result in results3) result.ReplaceWithTextRun("Replacement 3", true);

			var results4 = document.Body.FindText("{{placeholder4}}", StringComparison.InvariantCultureIgnoreCase).ToArray();
			Assert.Single(results4);
			foreach (var result in results4) result.ReplaceWithTextRun("Replacement\t4", true);

			var results5 = document.Body.FindText("{{Placeholder5}}", StringComparison.InvariantCulture).ToArray();
			Assert.Equal(2, results5.Length);
			foreach (var result in results5) result.ReplaceWithTextRun("Replacement 5");

			var defaultFooter = document.Body.Sections.First().DefaultFooter;
			Assert.NotNull(defaultFooter);
			var results6 = defaultFooter!.FindText("{{Placeholder6}}", StringComparison.InvariantCulture).ToArray();
			Assert.Single(results6);
			foreach (var result in results6) result.ReplaceWithTextRun("Replacement 6");

			SaveTempDocument(document, "SearchAndReplace_ExistingDocument.docx");
		}

		[Fact]
		public void ReplacementWithinSingleRun()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();

			var paragraph = section.AppendParagraph();
			paragraph.AppendText("text to be replaced}}-{{");

			var searchResult = paragraph.FindText("text to be replaced}}", StringComparison.InvariantCulture);
			var firstResult = searchResult.First();

			firstResult.ReplaceWithTextRun("THIS IS THE REPLACEMENT");

			SaveTempDocument(document, "SearchAndReplace_SingleRun.docx");
		}

		[Fact]
		public void ReplacementInThreeRuns()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();

			var paragraph = section.AppendParagraph();
			paragraph.AppendText("This is some text and this is a text to").Properties.Bold = true;
			paragraph.AppendText(" be re").Properties.Underline.Pattern = UnderlinePattern.Single;
			paragraph.AppendText("placed and this is some text after. ").Properties.Italic = true;

			var searchResult = paragraph.FindText("text to be replaced", StringComparison.InvariantCulture);
			var firstResult = searchResult.First();

			firstResult.ReplaceWithTextRun("THIS IS THE REPLACEMENT");

			SaveTempDocument(document, "SearchAndReplaceTreeRuns.docx");
		}

		[Fact]
		public void KeepFirstRunFormatting()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();

			var paragraph = section.AppendParagraph();
			paragraph.AppendText("This is some text and this is a text to").Properties.Bold = true;
			paragraph.AppendText(" be re").Properties.Underline.Pattern = UnderlinePattern.Single;
			paragraph.AppendText("placed and this is some text after. ").Properties.Italic = true;

			var searchResult = paragraph.FindText("text to be replaced", StringComparison.InvariantCulture);
			var firstResult = searchResult.First();

			firstResult.ReplaceWithTextRun("THIS IS THE REPLACEMENT", true);

			SaveTempDocument(document, "SearchAndReplace_KeepFirstRunFormatting.docx");
		}
	}
}
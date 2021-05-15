using System.Linq;
using Wordroller.Content.Lists;
using Xunit;
using Xunit.Abstractions;

namespace Wordroller.Tests
{
	public class PageNumbersTests : TestsBase
	{
		public PageNumbersTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public void AddCurrentPageNumberAndNumberOfDocumentPages()
		{
			using var document = TestHelper.CreateNewDocument();

			document.Styles.DocumentDefaults.RunProperties.Font.Ascii = "Times New Roman";
			document.Styles.DocumentDefaults.RunProperties.Font.HighAnsi = "Arial";
			document.Styles.DocumentDefaults.ParagraphProperties.Spacing.BetweenLinesLn = 1.5;

			var section = document.Body.Sections.First();

			var paragraph1 = section.AppendParagraph();
			paragraph1.AppendText("This is page ");
			paragraph1.AppendCurrentPageNumber("+##");
			paragraph1.AppendText(" of ");
			paragraph1.AppendNumberOfDocumentPages(NumberFormat.Decimal);
			paragraph1.AppendText(".");

			SaveTempDocument(document, "PageNumbers.docx");
		}
	}
}
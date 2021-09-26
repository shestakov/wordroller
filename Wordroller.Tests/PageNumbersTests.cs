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
			var section = document.Body.Sections.First();

			var paragraph = section.AppendParagraph();
			paragraph.AppendText("This is page ");
			paragraph.AppendCurrentPageNumber("+##", false, true);
			paragraph.AppendText(" of ");
			paragraph.AppendNumberOfDocumentPages(NumberFormat.Decimal, false, true);
			paragraph.AppendText(".");

			SaveTempDocument(document, "PageNumbers.docx");
		}
	}
}
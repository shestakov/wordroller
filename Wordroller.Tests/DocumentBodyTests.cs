using System.Linq;
using Wordroller.Content.HeadersAndFooters;
using Wordroller.Content.Properties.Sections;
using Wordroller.Content.Properties.Sections.PageSizes;
using Xunit;
using Xunit.Abstractions;

namespace Wordroller.Tests
{
	public class DocumentBodyTests : TestsBase
	{
		public DocumentBodyTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public void CreateEmptyDocument()
		{
			using var document = TestHelper.CreateNewDocument();
			SaveTempDocument(document, "DocumentBody_Empty.docx");
		}

		[Fact]
		public void CreateEmptyHeader()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();
			var header = section.CreateHeader(FooderDesignation.Default);
			var paragraph = header.Paragraphs.First();
			paragraph.Remove();
			Assert.Empty(header.Paragraphs);
			SaveTempDocument(document, "DocumentBody_EmptyHeader.docx");
		}

		[Fact]
		public void StartNewSection()
		{
			using var document = TestHelper.CreateNewDocument();

			var content = document.Body.Content;

			var paragraph1 = content.AppendParagraph();
			paragraph1.AppendText("This is the first document section");

			content.LastSectionProperties.Size.Orientation = PageOrientation.Landscape;
			content.LastSectionProperties.Size.WidthCm = 15;
			content.LastSectionProperties.Size.HeightCm = 5;

			content.StartNewSection(SectionBreakType.EvenPage);

			var paragraph2 = content.AppendParagraph();
			paragraph2.AppendText("This is the second document section");

			content.LastSectionProperties.Size.Orientation = PageOrientation.Portrait;
			content.LastSectionProperties.Size.WidthCm = 10;
			content.LastSectionProperties.Size.HeightCm = 12;

			SaveTempDocument(document, "DocumentBody_StartNewSection.docx");
		}
	}
}
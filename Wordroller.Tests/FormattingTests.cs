using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Wordroller.Tests
{
	public class FormattingTests : TestsBase
	{
		public FormattingTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public void CreateText()
		{
			using var document = TestHelper.CreateNewDocument();

			document.Styles.DocumentDefaults.RunProperties.Font.Ascii = "Times New Roman";
			document.Styles.DocumentDefaults.RunProperties.Font.HighAnsi = "Arial";
			document.Styles.DocumentDefaults.ParagraphProperties.Spacing.BetweenLinesLn = 1.5;

			var section = document.Body.Sections.First();

			var paragraph1 = section.AppendParagraph();
			paragraph1.AppendText("This is the ASCII text in the default Times New Roman font");
			paragraph1.AppendText("\n");
			paragraph1.AppendText("А это кириллический текст другим шрифтом");

			var paragraph2 = section.AppendParagraph();
			var run2 = paragraph2.AppendText("This is a text with a font different form default");
			run2.Properties.Font.Ascii = "Helvetica";

			SaveTempDocument(document, "TextDocument.docx");
		}
	}
}
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

			SaveTempDocument(document, "Formatting_Text.docx");
		}

		[Fact]
		public void BordersAndColors()
		{
			using var document = TestHelper.CreateNewDocument();

			var section = document.Body.Sections.First();

			var paragraph1 = section.AppendParagraph();
			paragraph1.AppendText("This is the first paragraph with red dotted borders and pink fill");
			paragraph1.AppendText("\n");
			paragraph1.AppendText("А это кириллический текст в первом параграфе");
            paragraph1.Properties.Shading.Fill = "FF8080";
            // Actually paragraph could have both variants of borders settings, but here implemented the first only:
            // 1. every border part (top/right/bottom/left) have own settings
            // 2. all properties in one border element at once
            paragraph1.Properties.Borders.Top.Color = "FF0000";
            paragraph1.Properties.Borders.Right.Color = "FF0000";
            paragraph1.Properties.Borders.Bottom.Color = "FF0000";
            paragraph1.Properties.Borders.Left.Color = "FF0000";
            paragraph1.Properties.Borders.Top.WidthEp = 16; // 16 EightsParts is 2
            paragraph1.Properties.Borders.Right.WidthEp = 16; // 16 EightsParts is 2
            paragraph1.Properties.Borders.Bottom.WidthEp = 16; // 16 EightsParts is 2
            paragraph1.Properties.Borders.Left.WidthEp = 16; // 16 EightsParts is 2
            paragraph1.Properties.Borders.Top.Style = Content.Properties.Borders.LineBorderStyle.Dotted;
            paragraph1.Properties.Borders.Right.Style = Content.Properties.Borders.LineBorderStyle.DotDash;
            paragraph1.Properties.Borders.Bottom.Style = Content.Properties.Borders.LineBorderStyle.DashDotStroked;
            paragraph1.Properties.Borders.Left.Style = Content.Properties.Borders.LineBorderStyle.DotDotDash;

            var paragraph2 = section.AppendParagraph();
			paragraph2.AppendText("This is a new paragraph with normal style and with ");
			var run2 = paragraph2.AppendText("run having own green style");
            run2.Properties.Shading.Fill = "80FF80";
            run2.Properties.Color.Color = "008000"; // Text color set with Color.Color!
            // Run could have only common properties for all border parts in one xml item brd
            run2.Properties.Border.Color = "00FF00";
            run2.Properties.Border.WidthEp = 24;
            run2.Properties.Border.Style = Content.Properties.Borders.LineBorderStyle.Double;

            SaveTempDocument(document, "Formatting_BordersAndColors.docx");
		}
	}
}
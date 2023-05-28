using System.Linq;
using Wordroller.Content.Properties;
using Wordroller.Content.Properties.Tables.Cells;
using Wordroller.Content.Tables;
using Wordroller.Styles;
using Xunit;
using Xunit.Abstractions;

namespace Wordroller.Tests
{
	public class TableTests : TestsBase
	{
		public TableTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public void VerticalMerge()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();

			var table = section.AppendTable(new CreateTableParameters(WidthUnit.Pc, 100, 3, WidthUnit.Pc, new double[] { 33f, 33f, 33f },
				DefaultTableStyles.TableNormal));

			var rows = table.Rows.ToArray();

			var cell1 = rows[0].Cells.First();
			cell1.Paragraphs.First().AppendText("Cell (1, 1)");
			cell1.Properties.VerticalMerge = CellVerticalMerge.Restart;

			var cell2 = rows[1].Cells.First();
			cell2.Properties.VerticalMerge = CellVerticalMerge.Continue;

			SaveTempDocument(document, "Table_VerticalMerge.docx");
		}

		[Fact]
		public void HorizontalMerge()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();

			var table = section.AppendTable(new CreateTableParameters(WidthUnit.Pc, 100, 3, WidthUnit.Pc, new double[] { 33f, 33f, 33f },
				DefaultTableStyles.TableNormal));

			var row = table.Rows.First();
			var cells = row.Cells.ToArray();

			var cell1 = cells[0];
			cell1.Paragraphs.First().AppendText("Cell (1, 1)");
			cell1.Properties.GridSpan = 2;

			var cell2 = cells[1];
			cell2.Remove();

			SaveTempDocument(document, "Table_HorizontalMerge.docx");
		}

		[Fact]
		public void Table_Width_SumOfColumnWidths()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();

			var table = section.AppendTable(new CreateTableParameters(3, WidthUnit.Cm, new double[] { 1f, 2f, 3f }, DefaultTableStyles.TableNormal));
			var row = table.Rows.First();
			var cells = row.Cells.ToArray();

			cells[0].Paragraphs.First().AppendText("Cell (1, 1)");
			cells[1].Paragraphs.First().AppendText("Cell (1, 2)");
			cells[2].Paragraphs.First().AppendText("Cell (1, 3)");

			SaveTempDocument(document, "Table_Width_SumOfColumnWidths.docx");
		}

		[Fact]
		public void Table_Width_Fixed()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();

			var table = section.AppendTable(new CreateTableParameters(WidthUnit.Cm, 8f, 3, WidthUnit.Cm, new double[] { 1f, 1f, 2f },
				DefaultTableStyles.TableNormal));
			var row = table.Rows.First();
			var cells = row.Cells.ToArray();

			cells[0].Paragraphs.First().AppendText("Cell (1, 1)");
			cells[1].Paragraphs.First().AppendText("Cell (1, 2)");
			cells[2].Paragraphs.First().AppendText("Cell (1, 3)");

			SaveTempDocument(document, "Table_Width_Fixed.docx");
		}

		[Fact]
		public void Table_CellShading_Color()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();

			var table = section.AppendTable(new CreateTableParameters(3, WidthUnit.Cm, new double[] { 1f, 2f, 3f }, DefaultTableStyles.TableNormal));

			var row = table.Rows.First();
			var cells = row.Cells.ToArray();

			cells[0].Paragraphs.First().AppendText("Cell (1, 1)");
			cells[1].Paragraphs.First().AppendText("Cell (1, 2)");
			cells[2].Paragraphs.First().AppendText("Cell (1, 3)");

			var cell1 = cells[0];
			cell1.Properties.Shading.Fill = "00FF00";

			var cell2 = cells[1];
			cell2.Properties.Shading.Color = "FF0000";
			cell2.Properties.Shading.Fill = "00FF00";
			cell2.Properties.Shading.ShadingPattern = ShadingPattern.Pct50;

			var cell3 = cells[2];
			cell3.Properties.Shading.ThemeColor = ThemeColor.Accent6;
			cell3.Properties.Shading.ThemeFill = ThemeColor.Accent3;
			cell3.Properties.Shading.ShadingPattern = ShadingPattern.Pct20;

			SaveTempDocument(document, "Table_CellShading_Color.docx");
		}
	}
}
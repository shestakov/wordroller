namespace Wordroller.Content.Tables
{
	public class CreateTableParameters
	{
		public int Rows { get; private set; }
		public WidthUnit? TableWidthUnit { get; private set; }
		public double? TableWidth { get; private set; }
		public WidthUnit ColumnWidthUnit { get; private set; }
		public double[] ColumnWidths { get; private set; }
		public string Style { get; private set; }

		public CreateTableParameters(WidthUnit tableWidthUnit, double tableWidth, int rows, WidthUnit columnWidthUnit, double[] columnWidths,
			string style)
		{
			Rows = rows;
			TableWidthUnit = tableWidthUnit;
			TableWidth = tableWidth;
			ColumnWidthUnit = columnWidthUnit;
			ColumnWidths = columnWidths;
			Style = style;
		}

		public CreateTableParameters(int rows, WidthUnit columnWidthUnit, double[] columnWidths, string style)
		{
			Rows = rows;
			ColumnWidthUnit = columnWidthUnit;
			ColumnWidths = columnWidths;
			Style = style;
		}
	}

	public enum WidthUnit
	{
		Pc,
		Twip,
		Cm,
		Inch
	}
}
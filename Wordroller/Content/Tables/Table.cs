using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Tables;
using Wordroller.Content.Properties.Tables.Widths;
using Wordroller.Content.Text;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Tables
{
	public class Table : DocumentContentElement, ITablePropertiesContainer
	{
		internal Table(XElement xml, DocumentContentContainer parent) : base(xml, parent)
		{
		}

		public TableProperties Properties => new TableProperties(this, Xml.Element(XName.Get("tblPr", Namespaces.w.NamespaceName)));

		public IEnumerable<Paragraph> Paragraphs => Rows
			.SelectMany(r => r.Cells)
			.SelectMany(c => c.AllParagraphsRecursive);

		public IEnumerable<Row> Rows
		{
			get
			{
				var rows = Xml.Elements(XName.Get("tr", Namespaces.w.NamespaceName))
					.Select(r => new Row(this, r))
					.ToArray();

				return rows;
			}
		}

		public void SetTableGrid(IEnumerable<int> columnWidthsTw)
		{
			var xNameTablePr = XName.Get("tblPr", Namespaces.w.NamespaceName);
			var tblPr = Xml.Element(xNameTablePr);

			var xName = Namespaces.w + "tblGrid";

			Xml.Element(xName)?.Remove();

			var tblGrid = new XElement(xName);

			foreach (var c in columnWidthsTw)
			{
				tblGrid.Add(new XElement(Namespaces.w + "gridCol", new XAttribute(Namespaces.w + "w", c)));
			}

			// Seems like LibreOffice expects tblGrid right after tblPr
			if (tblPr != null)
			{
				tblPr.AddAfterSelf(tblGrid);
			}
			else
			{
				Xml.AddFirst(tblGrid);
			}
		}

		internal static Table Create(CreateTableParameters parameters, DocumentContentContainer parent)
		{
			if (parameters.Rows < 1)
				throw new ArgumentOutOfRangeException(nameof(parameters.Rows), parameters.Rows, "Rows must be greater than 0");

			if (parameters.ColumnWidths.Length < 1)
				throw new ArgumentOutOfRangeException(nameof(parameters.ColumnWidths), parameters.ColumnWidths.Length, "ColumnWidths must not be empty");

			var columnWidths = parameters.ColumnWidths;
			var columnWidthUnit = parameters.ColumnWidthUnit;

			int[] computedColumnWidths = columnWidths.Select(value => ComputeWidth(columnWidthUnit, value)).ToArray();

			if (columnWidthUnit == WidthUnit.Pc) // Fixing the last width so the sum of Pct widths equals 5000 for LibreOffice
			{
				computedColumnWidths[computedColumnWidths.Length - 1] += 5000 - computedColumnWidths.Sum();
			}

			var tbl = new XElement(Namespaces.w + "tbl");

			// //This code below is commented out as it does not actually impact behavior
			// if (parameters.ColumnWidthUnit != WidthUnit.Pc)
			// {
			// 	var tableGrid = new XElement(Namespaces.w + "tblGrid");
			// 	foreach (var c in computedColumnWidths)
			// 		tableGrid.Add(new XElement(Namespaces.w + "gridCol", new XAttribute(Namespaces.w + "w", c)));
			//
			// 	tbl.Add(tableGrid);
			// }

			for (var r = 0; r < parameters.Rows; r++)
			{
				var row = new XElement(Namespaces.w + "tr");

				foreach (var c in computedColumnWidths)
				{
					var cell = CreateTableCell(columnWidthUnit == WidthUnit.Pc, c);
					row.Add(cell);
				}

				tbl.Add(row);
			}

			var table = new Table(tbl, parent);

			var properties = table.Properties;

			properties.StyleId = parameters.Style;
			properties.LayoutType = TableLayoutType.Fixed;

			if (parameters.TableWidth.HasValue && parameters.TableWidthUnit.HasValue)
			{
				if (parameters.TableWidthUnit == WidthUnit.Pc)
				{
					properties.PreferredWidthFp = ComputeWidth(WidthUnit.Pc, parameters.TableWidth.Value);
				} else
				{
					properties.PreferredWidthTw = ComputeWidth(parameters.TableWidthUnit.Value, parameters.TableWidth.Value);
				}
			}

			return table;
		}

		XElement ITablePropertiesContainer.GetOrCreateTablePropertiesXmlElement()
		{
			var xName = XName.Get("tblPr", Namespaces.w.NamespaceName);
			var tblPr = Xml.Element(xName);
			if (tblPr != null) return tblPr;
			tblPr = new XElement(xName);
			Xml.AddFirst(tblPr);
			return tblPr;
		}

		private static XElement CreateTableCell(bool perCent, int width)
		{
			var tcPr = new XElement(XName.Get("tcPr", Namespaces.w.NamespaceName));

			var tc = new XElement
			(
				XName.Get("tc", Namespaces.w.NamespaceName),
				tcPr,
				new XElement(XName.Get("p", Namespaces.w.NamespaceName),
					new XElement(XName.Get("pPr", Namespaces.w.NamespaceName)))
			);

			if (perCent)
				tcPr.SetTableWidthFpValue("tcW", width);
			else
				tcPr.SetTableWidthTwValue("tcW", width);

			return tc;
		}

		private static int ComputeWidth(WidthUnit widthUnit, double value)
		{
			return widthUnit switch
			{
				WidthUnit.Pc => (int) (value * UnitHelper.FipcsPerPc),
				WidthUnit.Twip => (int) value,
				WidthUnit.Cm => (int) (value * UnitHelper.TwipsPerCm),
				WidthUnit.Inch => (int) (value * UnitHelper.TwipsPerInch),
				_ => throw new ArgumentOutOfRangeException(nameof(widthUnit), widthUnit, "Unsupported column width unit")
			};
		}
	}
}
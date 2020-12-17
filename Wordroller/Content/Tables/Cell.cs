using System;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Tables.Cells;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Tables
{
	public class Cell : DocumentContentContainer, ICellPropertiesContainer
	{
		private readonly Row row;

		internal Cell(Row row, XElement xml, PackagePart packagePart, WordDocument document) : base(xml, packagePart, document)
		{
			this.row = row;
		}

		public CellProperties Properties => new CellProperties(this, Xml.Element(XName.Get("tcPr", Namespaces.w.NamespaceName)));

		public override Table AppendTable(CreateTableParameters parameters)
		{
			var table = base.AppendTable(parameters);
			AppendParagraph(); // This is critical for correct behavior
			return table;
		}

		public override void Remove()
		{
			if (row.Cells.Count() < 2) throw new InvalidOperationException("The last table cell in a row cannot be deleted");
			base.Remove();
		}

		XElement ICellPropertiesContainer.GetOrCreateCellPropertiesXmlElement()
		{
			var xName = XName.Get("tcPr", Namespaces.w.NamespaceName);
			var tcPr = Xml.Element(xName);
			if (tcPr != null) return tcPr;
			tcPr = new XElement(xName);
			Xml.AddFirst(tcPr);
			return tcPr;
		}
	}
}
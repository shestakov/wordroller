using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Tables.Rows;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Tables
{
	public class Row : XmlElementWrapper, IRowPropertiesContainer
	{
		private readonly Table table;

		internal Row(Table table, XElement xml) : base(xml)
		{
			this.table = table;
		}

		public RowProperties Properties => new RowProperties(this, Xml.Element(XName.Get("trPr", Namespaces.w.NamespaceName)));

		public IEnumerable<Cell> Cells
		{
			get
			{
				var cells = Xml.Elements(XName.Get("tc", Namespaces.w.NamespaceName))
					.Select(c => new Cell(this, c, table.ParentContainer.PackagePart, table.ParentContainer.Document));

				return cells;
			}
		}

		XElement IRowPropertiesContainer.GetOrCreateRowPropertiesXmlElement()
		{
			var xName = XName.Get("trPr", Namespaces.w.NamespaceName);
			var trPr = Xml.Element(xName);
			if (trPr != null) return trPr;
			trPr = new XElement(xName);
			Xml.AddFirst(trPr);
			return trPr;
		}

		public override void Remove()
		{
			if (table.Rows.Count() < 2) throw new InvalidOperationException("The last table row cannot be deleted");
			base.Remove();
		}
	}
}
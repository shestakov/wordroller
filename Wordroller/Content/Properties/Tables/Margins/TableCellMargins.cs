using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Tables.Margins
{
	public class TableCellMargins : TableMarginsBase
	{
		private readonly ITableCellMarginsContainer container;

		internal TableCellMargins(ITableCellMarginsContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "tblCellMar") throw new ArgumentException($"XML element must be tblCellMar but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateTableCellMarginsXmlElement();
		}
	}
}
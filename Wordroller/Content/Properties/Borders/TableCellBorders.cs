using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Borders
{
	public class TableCellBorders : CommonBordersBase
	{
		private readonly ITableCellBordersContainer container;

		internal TableCellBorders(ITableCellBordersContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "tcBorders") throw new ArgumentException($"XML element must be tcBorders but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public BorderElement InnerHorizontal => new BorderElement("insideH", this, Xml?.Element(Namespaces.w + "insideH"));

		public BorderElement InnerVertical => new BorderElement("insideV", this, Xml?.Element(Namespaces.w + "insideV"));

		public BorderElement TopLeftToBottomRight => new BorderElement("tl2br", this, Xml?.Element(Namespaces.w + "tl2br"));

		public BorderElement TopRightToBottomLeft => new BorderElement("tr2bl", this, Xml?.Element(Namespaces.w + "tr2bl"));

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateBordersXmlElement();
		}
	}
}
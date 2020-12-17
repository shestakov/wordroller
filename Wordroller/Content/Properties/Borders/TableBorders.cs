using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Borders
{
	public class TableBorders : CommonBordersBase
	{
		private readonly ITableBordersContainer container;

		internal TableBorders(ITableBordersContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "tblBorders") throw new ArgumentException($"XML element must be tblBorders but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public BorderElement InnerHorizontal => new BorderElement("insideH", this, Xml?.Element(Namespaces.w + "insideH"));

		public BorderElement InnerVertical => new BorderElement("insideV", this, Xml?.Element(Namespaces.w + "insideV"));

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateBordersXmlElement();
		}
	}
}
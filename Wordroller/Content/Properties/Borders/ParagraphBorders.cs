using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Borders
{
	public class ParagraphBorders : CommonBordersBase
	{
		private readonly IParagraphBordersContainer container;

		internal ParagraphBorders(IParagraphBordersContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "pBdr") throw new ArgumentException($"XML element must be pBdr but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public BorderElement Between => new BorderElement("between", this, Xml?.Element(Namespaces.w + "between"));

		public BorderElement Bar => new BorderElement("bar", this, Xml?.Element(Namespaces.w + "bar"));

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateBordersXmlElement();
		}
	}
}
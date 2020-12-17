using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Borders
{
	public class PageBorders : CommonBordersBase
	{
		private readonly IPageBordersContainer container;

		internal PageBorders(IPageBordersContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "pgBorders") throw new ArgumentException($"XML element must be pgBorders but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateBordersXmlElement();
		}
	}
}